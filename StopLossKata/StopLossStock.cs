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
            _limit = _currentPrice - _offset;
            _bus = bus;
        }

        public void Handle(PriceChanged priceChanged)
        {
            _correlationId += 1;
            if (priceChanged.NewPrice < _limit)
            {
                var id = _correlationId;
                _bus.Publish(new Timeout(30, () => PriceDropTimeout(id)));
            }

            if (priceChanged.NewPrice > _currentPrice)
            {
                var id = _correlationId;
                _bus.Publish(new Timeout(15, () => SetNewLimitTimout(id, priceChanged.NewPrice - _offset)));
            }

            _currentPrice = priceChanged.NewPrice;
        }

        private void PriceDropTimeout(int correlationId)
        {
            if (_correlationId == correlationId)
                _bus.Publish(new TriggerStockLoss());
        }

        private void SetNewLimitTimout(int correlationId, int limit)
        {
            if (_correlationId == correlationId)
                _limit = limit;
        }
    }
}