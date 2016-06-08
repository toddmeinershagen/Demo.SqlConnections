using System;
using System.Diagnostics;

namespace Demo.SqlConnections.WinForm
{
    public class Counter : IDisposable
    {
        private readonly PerformanceCounter _counter;

        public Counter(PerformanceCounter counter)
        {
            _counter = counter;
        }

        public float NextValue()
        {
            try
            {
                return _counter.NextValue();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public string CategoryName => _counter.CategoryName;
        public string CounterName => _counter.CounterName;
        public void Dispose()
        {
            _counter.Dispose();
        }
    }
}