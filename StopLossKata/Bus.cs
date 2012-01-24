using System.Collections.Generic;

namespace StopLossKata
{
    public interface IBus
    {
        void Publish(object message);
    }

    /// <summary>
    /// Bus and timeout service should be separated
    /// </summary>
    public class FakeBus : IBus
    {
        public readonly List<object> Messages = new List<object>();
        private readonly List<Timeout> _timeouts = new List<Timeout>();

        public void Publish(object message)
        {
            var timeout = message as Timeout;
            if (timeout != null)
                _timeouts.Add(timeout);
            else
                Messages.Add(message);
        }

        public void TimewarpSeconds(int seconds)
        {
            foreach (var timeout in _timeouts.ToArray())
            {
                timeout.Delay -= seconds;
                if (timeout.Delay <= 0)
                {
                    timeout.Callback();
                    _timeouts.Remove(timeout);
                }
            }
        }
    }
}