using Machine.Specifications;

namespace StopLossKata
{
    [Subject(typeof (StopLossStock))]
    public class When_buying_stop_loss_stock_without_price_change
    {
        Establish context = () => bus = new FakeBus();

        Because of = () =>
        {
            stock = new StopLossStock(10, 1, bus);
        };

        It should_not_trigger_stock_loss = () => bus.ShouldNotTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_drops_below_limit_for_30_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
        };

        Because of = () =>
        {
            stock.Handle(new PriceChanged(8));
            bus.TimewarpSeconds(30);
        };

        It should_trigger_stock_loss = () => bus.ShouldTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_drops_below_limit_for_less_than_30_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
        };

        Because of = () =>
        {
            stock.Handle(new PriceChanged(8));
            bus.TimewarpSeconds(29);
        };

        It should_not_trigger_stock_loss = () => bus.ShouldNotTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_drops_within_limit
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
        };

        Because of = () => stock.Handle(new PriceChanged(9));

        It should_not_trigger_stock_loss = () => bus.ShouldNotTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_raises_and_drops_below_new_trailing_limit_for_30_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
            stock.Handle(new PriceChanged(11));
        };

        Because of = () =>
        {
            stock.Handle(new PriceChanged(9));
            bus.TimewarpSeconds(30);
        };

        It should_trigger_stock_loss = () => bus.ShouldTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }
}
