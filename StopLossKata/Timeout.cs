using System;

namespace StopLossKata
{
    public class Timeout
    {
        readonly Action _callback;

        public Timeout(int delay, Action callback)
        {
            Delay = delay;
            _callback = callback;
        }

        public Action Callback
        {
            get
            {
                return _callback;
            }
        }

        public int Delay { get; set; }
    }
}