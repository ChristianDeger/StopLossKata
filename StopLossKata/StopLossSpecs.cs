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
    public class When_buying_stop_loss_stock_and_price_drops_below_limit_for_29_seconds_and_raises_and_drops_below_again_for_29_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
            stock.Handle(new PriceChanged(8));
            bus.TimewarpSeconds(29);
            stock.Handle(new PriceChanged(10));
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
    public class When_buying_stop_loss_stock_and_price_drops_within_limit_for_30_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
        };

        Because of = () =>
        {
            stock.Handle(new PriceChanged(9));
            bus.TimewarpSeconds(30);
        };

        It should_not_trigger_stock_loss = () => bus.ShouldNotTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_drops_below_limit_for_29_seconds_and_raises_again_for_30_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
            stock.Handle(new PriceChanged(8));
            bus.TimewarpSeconds(29);
        };

        Because of = () =>
        {
            stock.Handle(new PriceChanged(10));
            bus.TimewarpSeconds(30);
        };

        It should_not_trigger_stock_loss = () => bus.ShouldNotTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_raises_for_14_seconds_and_drops_below_possible_new_trailing_limit_for_30_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
            stock.Handle(new PriceChanged(11));
            bus.TimewarpSeconds(14);
        };

        Because of = () =>
        {
            stock.Handle(new PriceChanged(9));
            bus.TimewarpSeconds(30);
        };

        It should_not_trigger_stock_loss = () => bus.ShouldNotTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_raises_for_15_seconds_and_drops_below_new_trailing_limit_for_30_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
            stock.Handle(new PriceChanged(11));
            bus.TimewarpSeconds(15);
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

    [Subject(typeof(StopLossStock))]
    public class When_buying_stop_loss_stock_and_price_raises_for_14_seconds_drops_for_1_second_within_limit_and_drops_below_old_trailing_limit_for_30_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
            stock.Handle(new PriceChanged(12));
            bus.TimewarpSeconds(14);
            stock.Handle(new PriceChanged(11));
            bus.TimewarpSeconds(1);
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
    public class When_buying_stop_loss_stock_and_price_raises_for_14_seconds_drops_for_1_second_within_limit_and_drops_within_old_trailing_limit_for_30_seconds
    {
        Establish context = () =>
        {
            bus = new FakeBus();
            stock = new StopLossStock(10, 1, bus);
            stock.Handle(new PriceChanged(12));
            bus.TimewarpSeconds(14);
            stock.Handle(new PriceChanged(11));
            bus.TimewarpSeconds(1);
        };

        Because of = () =>
        {
            stock.Handle(new PriceChanged(9));
            bus.TimewarpSeconds(30);
        };

        It should_not_trigger_stock_loss = () => bus.ShouldNotTriggerStopLoss();

        static StopLossStock stock;
        static FakeBus bus;
    }
}
