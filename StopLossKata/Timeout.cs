using System;

namespace StopLossKata
{
    public class Timeout
    {
        readonly int _delay;
        readonly Action _callback;

        public Timeout(int delay, Action callback)
        {
            _delay = delay;
            _callback = callback;
        }

        public Action Callback
        {
            get
            {
                return _callback;
            }
        }

        public int Delay
        {
            get
            {
                return _delay;
            }
        }
    }
}