namespace StopLossKata
{
    public class StopLossStock
    {
        readonly int _limit;
        readonly IBus _bus;

        public StopLossStock(int limit, IBus bus)
        {
            _limit = limit;
            _bus = bus;
        }

        public void Handle(PriceChange priceChange)
        {
            if (priceChange.NewPrice <= _limit)
                _bus.Publish(new TriggerStockLoss());
        }
    }
}