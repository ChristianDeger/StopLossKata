using System.Collections.Generic;
using System.Linq;

namespace StopLossKata
{
    public interface IBus
    {
        void Publish(object message);
    }

    public class FakeBus : IBus
    {
        public readonly List<object> Messages = new List<object>();
        private readonly List<Timeout> _timeouts = new List<Timeout>();
        int _runningTimeInSeconds = 0;

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
            _runningTimeInSeconds += seconds;
            var callbacks = _timeouts.Where(t => t.Delay <= _runningTimeInSeconds).Select(t => t.Callback);
            foreach (var callback in callbacks)
            {
                callback();
            }
        }
    }
}