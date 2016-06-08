using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Demo.SqlConnections.WinForm
{
    public class CounterRepository : IDisposable
    {
        private bool _disposed;
        private readonly string _instanceName;
        private readonly Dictionary<CounterKey, Counter> _counters = new Dictionary<CounterKey, Counter>();
          
        public CounterRepository(string instanceName = null)
        {
            _instanceName = instanceName;
        }

        public Counter GetCounter(CounterKey key)
        {
            if (_counters.ContainsKey(key))
                return _counters[key];

            var innerCounter = new PerformanceCounter {CategoryName = key.CategoryName, CounterName = key.CounterName};
            if (_instanceName != null)
            {
                innerCounter.InstanceName = _instanceName;
            }

            var counter = new Counter(innerCounter);
            _counters.Add(key, counter);

            return counter;
        }

        public void Close()
        {
            foreach (var counter in _counters)
            {
                counter.Value.Dispose();
            }

            _counters.Clear();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
                Close();

            _disposed = true;
        }

        ~CounterRepository()
        {
            Dispose(false);
        }
    }
}