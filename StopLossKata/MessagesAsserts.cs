using System.Collections.Generic;

using Machine.Specifications;

namespace StopLossKata
{
    public static class MessagesAsserts
    {
        public static void ShouldTriggerStopLoss(this FakeBus bus)
        {
            bus.Messages.ShouldContain(m => m.GetType() == typeof(TriggerStockLoss));
        }

        public static void ShouldNotTriggerStopLoss(this FakeBus bus)
        {
            bus.Messages.ShouldNotContain(m => m.GetType() == typeof(TriggerStockLoss));
        }
    }
}