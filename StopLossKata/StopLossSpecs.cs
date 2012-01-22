using Machine.Specifications;

namespace StopLossKata
{
    [Subject(typeof (StopLossStock))]
    public class When_buying_stop_loss_stock_without_price_change
    {
        Because of = () => { stock = new StopLossStock(10, 1, () => stockLossTriggered = true); };

        It should_not_trigger_stock_loss = () => stockLossTriggered.ShouldBeFalse();

        static StopLossStock stock;
        static bool stockLossTriggered;
    }
}
