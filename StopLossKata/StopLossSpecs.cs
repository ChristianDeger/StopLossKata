using Machine.Specifications;

namespace StopLossKata
{
    [Subject(typeof (StopLossStock))]
    public class When_buying_stop_loss_stock_without_price_change
    {
        Because of = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
        };

        It should_not_trigger_stock_loss = () => bus.Messages.ShouldNotContain(m => m.GetType() == typeof(TriggerStockLoss));

        static StopLossStock stock;
        static bool stockLossTriggered;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_drops_beyond_limit
    {
        Because of = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
            stock.Handle(new PriceChange(9));
        };

        It should_trigger_stock_loss = () => bus.Messages.ShouldContain(m => m.GetType() == typeof(TriggerStockLoss));

        static StopLossStock stock;
        static bool stockLossTriggered;
        static FakeBus bus;
    }
}
