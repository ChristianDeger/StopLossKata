namespace StopLossKata
{
    public class StopLossStock
    {
        readonly int _limit;
        readonly IBus _bus;
        int _currentPrice;

        public StopLossStock(int initialPrice, int limit, IBus bus)
        {
            _currentPrice = initialPrice;
            _limit = limit;
            _bus = bus;
        }

        public void Handle(PriceChange priceChange)
        {
            if (priceChange.NewPrice < _currentPrice - _limit)
            {
                _bus.Publish(new TriggerStockLoss());
            }

            _currentPrice = priceChange.NewPrice;
        }
    }
}