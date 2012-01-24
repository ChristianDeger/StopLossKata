namespace StopLossKata
{
    public class StopLossStock
    {
        readonly int _offset;
        readonly IBus _bus;
        int _currentPrice;
        int _limit;
        int _correlationId;

        public StopLossStock(int initialPrice, int offset, IBus bus)
        {
            _currentPrice = initialPrice;
            _offset = offset;
            _limit = CalculateLimit(initialPrice);
            _bus = bus;
        }

        public void Handle(PriceChanged priceChanged)
        {
            _correlationId += 1;
            if (priceChanged.NewPrice < _limit)
            {
                var id = _correlationId;
                _bus.Publish(new Timeout(30, () => PriceDrop(id)));
            }

            if (priceChanged.NewPrice > _currentPrice)
            {
                var id = _correlationId;
                _bus.Publish(new Timeout(15, () => SetNewLimit(id, priceChanged.NewPrice)));
            }

            _currentPrice = priceChanged.NewPrice;
        }

        void PriceDrop(int correlationId)
        {
            if (_correlationId == correlationId)
            {
                _bus.Publish(new TriggerStockLoss());
            }
        }

        void SetNewLimit(int correlationId, int price)
        {
            if (_correlationId == correlationId)
            {
                _limit = CalculateLimit(price);
            }
        }

        int CalculateLimit(int price)
        {
            return price - _offset;
        }
    }
}