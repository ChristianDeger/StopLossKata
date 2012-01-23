using Machine.Specifications;

namespace StopLossKata
{
    [Subject(typeof (StopLossStock))]
    public class When_buying_stop_loss_stock_without_price_change
    {
        Establish context = () => bus = new FakeBus();

        Because of = () =>
        {
            stock = new StopLossStock(9, bus);
        };

        It should_not_trigger_stock_loss = () => bus.ShouldNotTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_drops_beyond_limit
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(9, bus);
        };

        Because of = () => stock.Handle(new PriceChange(9));

        It should_trigger_stock_loss = () => bus.ShouldTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_drops_within_limit
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(8, bus);
        };

        Because of = () => stock.Handle(new PriceChange(9));

        It should_not_trigger_stock_loss = () => bus.ShouldNotTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }
}
