using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace FubuMVC.Diagnostics.Instrumentation.Diagnostics
{
    public class RouteInstrumentationReport
    {
        private long _exceptionCount;
        private long _hitCount;
        private long _minExecutionTime = long.MaxValue;
        private long _maxExecutionTime;
        private long _totalExecutionTime;
        private ConcurrentDictionary<Guid, IList<string>> _exceptions;

        public decimal AverageExecutionTime { get { return _totalExecutionTime * 1m / _hitCount; } }
        public long ExceptionCount { get { return _exceptionCount; } }
        public long HitCount { get { return _hitCount; } }
        public long MinExecutionTime { get { return _minExecutionTime; } }
        public long MaxExecutionTime { get { return _maxExecutionTime; } }

        public string Route { get; private set; }

        public RouteInstrumentationReport()
        {
            _exceptions = new ConcurrentDictionary<Guid, IList<string>>();
        }

        public RouteInstrumentationReport(string route)
            : this()
        {
            Route = route;
        }

        public void IncrementHitCount()
        {
            Interlocked.Increment(ref _hitCount);
        }

        public void IncrementExceptionCount()
        {
            Interlocked.Increment(ref _exceptionCount);
        }

        public void AddExecutionTime(long executionTime)
        {
            Interlocked.Add(ref _totalExecutionTime, executionTime);

            if (executionTime < Interlocked.Read(ref _minExecutionTime))
            {
                Interlocked.Exchange(ref _minExecutionTime, executionTime);
            }

            if (executionTime > Interlocked.Read(ref _maxExecutionTime))
            {
                Interlocked.Exchange(ref _maxExecutionTime, executionTime);
            }
        }

        public void RecordException(Guid behaviorId, string exceptionText)
        {
            _exceptions.AddOrUpdate(behaviorId, text => new List<string>
            {
                exceptionText
            }, (id, exceptions) =>
            {
                exceptions.Add(exceptionText);
                return exceptions;
            });
        }
    }
}