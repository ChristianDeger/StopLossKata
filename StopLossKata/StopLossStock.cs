namespace StopLossKata
{
    public class StopLossStock
    {
        readonly int _limit;
        readonly IBus _bus;
        int _currentPrice;
        bool _allowPriceDropTimeout;

        public StopLossStock(int initialPrice, int limit, IBus bus)
        {
            _currentPrice = initialPrice;
            _limit = limit;
            _bus = bus;
        }

        public void Handle(PriceChanged priceChanged)
        {
            if (priceChanged.NewPrice < _currentPrice - _limit)
            {
                _bus.Publish(new Timeout(30, PriceDropTimeout));
                _allowPriceDropTimeout = true;
            }
            else
            {
                _allowPriceDropTimeout = false;
            }

            _currentPrice = priceChanged.NewPrice;
        }

        private void PriceDropTimeout()
        {
            if (_allowPriceDropTimeout)
                _bus.Publish(new TriggerStockLoss());
        }
    }
}