using System;

namespace StopLossKata
{
    public class StopLossStock
    {
        readonly IBus bus;

        public StopLossStock(int price, int limit, IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(PriceChange priceChange)
        {
            bus.Publish(new TriggerStockLoss());
        }
    }
}