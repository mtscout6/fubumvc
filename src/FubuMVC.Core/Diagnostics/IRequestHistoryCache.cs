using System.Collections.Generic;

namespace FubuMVC.Core.Diagnostics
{
    public interface IRequestHistoryCache : IDebugReportConsumer
    {
        IEnumerable<IDebugReport> RecentReports();
    }
}