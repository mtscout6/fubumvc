using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using FubuMVC.Core;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Diagnostics.Features.Requests;

namespace FubuMVC.Diagnostics.Instrumentation.Diagnostics
{
    public class RouteInstrumentationReport
    {
        private long _exceptionCount;
        private long _hitCount;
        private long _minExecutionTime = long.MaxValue;
        private long _maxExecutionTime;
        private long _totalExecutionTime;
        private readonly ConcurrentQueue<IDebugReport> _requestCache;
        private readonly DiagnosticsConfiguration _configuration;

        public decimal AverageExecutionTime { get { return _totalExecutionTime * 1m / _hitCount; } }
        public long ExceptionCount { get { return _exceptionCount; } }
        public long HitCount { get { return _hitCount; } }
        public long MinExecutionTime { get { return _minExecutionTime; } }
        public long MaxExecutionTime { get { return _maxExecutionTime; } }

        public string Route { get; private set; }

        public RouteInstrumentationReport(DiagnosticsConfiguration configuration)
        {
            _configuration = configuration;
            _requestCache = new ConcurrentQueue<IDebugReport>();
        }

        public RouteInstrumentationReport(string route, DiagnosticsConfiguration configuration)
            : this(configuration)
        {
            Route = route;
        }

        public void AddDebugReport(IDebugReport report)
        {
            var visitor = new RecordedRequestBehaviorVisitor();
            report.Steps.Each(s => s.Details.AcceptVisitor(visitor));

            if (visitor.HasExceptions())
            {
                IncrementExceptionCount();
            }

            IncrementHitCount();
            AddExecutionTime((long)report.ExecutionTime);

            _requestCache.Enqueue(report);

            while (_requestCache.Count > _configuration.MaxRequests)
            {
                IDebugReport r;
                _requestCache.TryDequeue(out r);
            }
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
    }
}