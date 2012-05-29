using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace FubuMVC.Core.Diagnostics
{
    public class RequestHistoryCache : IRequestHistoryCache
    {
        private readonly ConcurrentQueue<IDebugReport> _reports = new ConcurrentQueue<IDebugReport>();
        private readonly IEnumerable<ICacheFilter> _filters;
        private readonly DiagnosticsConfiguration _configuration;

        public RequestHistoryCache(IEnumerable<ICacheFilter> filters, DiagnosticsConfiguration configuration)
        {
            _filters = filters;
            _configuration = configuration;
        }

        // TODO -- let's thin this down from CurrentRequest
        public void AddReport(IDebugReport report, CurrentRequest request)
        {
            if(_filters.Any(f => f.Exclude(request)))
            {
                return;
            }

            _reports.Enqueue(report);
            while (_reports.Count > _configuration.MaxRequests)
            {
                IDebugReport r;
                _reports.TryDequeue(out r);
            }
        }

        public IEnumerable<IDebugReport> RecentReports()
        {
            return _reports.ToList();
        }
    }

    public interface ICacheFilter
    {
        bool Exclude(CurrentRequest request);
    }

    public class LambdaCacheFilter : ICacheFilter
    {
        private readonly Func<CurrentRequest, bool> _shouldExclude;

        public LambdaCacheFilter(Func<CurrentRequest, bool> shouldExclude)
        {
            _shouldExclude = shouldExclude;
        }

        public bool Exclude(CurrentRequest request)
        {
            return _shouldExclude(request);
        }
    }
}