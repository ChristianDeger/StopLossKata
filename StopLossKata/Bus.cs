using System.Collections.Generic;

namespace StopLossKata
{
    public interface IBus
    {
        void Publish(object message);
    }

    public class FakeBus : IBus
    {
        public List<object> Messages = new List<object>();

        public void Publish(object message)
        {
            Messages.Add(message);
        }
    }
}