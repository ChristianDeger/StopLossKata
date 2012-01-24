﻿namespace StopLossKata
{
    public class StopLossStock
    {
        readonly int _offset;
        readonly IBus _bus;
        int _currentPrice;
        int _limit;
        bool _allowPriceDropTimeout;

        public StopLossStock(int initialPrice, int offset, IBus bus)
        {
            _currentPrice = initialPrice;
            _offset = offset;
            _limit = _currentPrice - _offset;
            _bus = bus;
        }

        public void Handle(PriceChanged priceChanged)
        {
            if (priceChanged.NewPrice < _limit)
            {
                _bus.Publish(new Timeout(30, PriceDropTimeout));
                _allowPriceDropTimeout = true;
            }
            else
            {
                _allowPriceDropTimeout = false;
            }

            if (priceChanged.NewPrice > _currentPrice)
                _bus.Publish(new Timeout(15, () => SetNewLimitTimout(priceChanged.NewPrice - _offset)));

            _currentPrice = priceChanged.NewPrice;
        }

        private void PriceDropTimeout()
        {
            if (_allowPriceDropTimeout)
                _bus.Publish(new TriggerStockLoss());
        }

        private void SetNewLimitTimout(int limit)
        {
            _limit = limit;
        }
    }
}