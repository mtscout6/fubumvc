using System.Threading;

namespace FubuMVC.Diagnostics.Instrumentation.Diagnostics
{
    public class RouteInstrumentationReport
    {
        private long _hitCount;
        public long HitCount { get { return _hitCount; } }
        public string Url { get; private set; }

        public RouteInstrumentationReport(string url)
        {
            Url = url;
        }

        public void IncrementHitCount()
        {
            Interlocked.Increment(ref _hitCount);
        }
    }
}