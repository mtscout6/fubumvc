using System.Threading;

namespace FubuMVC.Diagnostics.Instrumentation.Diagnostics
{
    public class RouteInstrumentationReport
    {
        private long _hitCount;
        private long _totalExecutionTime;
        private long _minExecutionTime = long.MaxValue;
        private long _maxExecutionTime;
        
        public long HitCount { get { return _hitCount; } }
        public decimal AverageExecutionTime { get { return _totalExecutionTime * 1m / _hitCount; } }
        public long MinExecutionTime { get { return _minExecutionTime; } }
        public long MaxExecutionTime { get { return _maxExecutionTime; } }

        public string Route { get; private set; }

        public RouteInstrumentationReport()
        {

        }

        public RouteInstrumentationReport(string route)
        {
            Route = route;
        }

        public void IncrementHitCount()
        {
            Interlocked.Increment(ref _hitCount);
        }

        public void AddExecutionTime(long executionTime)
        {
            Interlocked.Add(ref _totalExecutionTime, executionTime);

            if (executionTime < Interlocked.Read(ref _minExecutionTime))
            {
                Interlocked.Exchange(ref _minExecutionTime, executionTime);
            }

            if ( executionTime > Interlocked.Read(ref _maxExecutionTime))
            {
                Interlocked.Exchange(ref _maxExecutionTime, executionTime); 
            }
        }
    }
}