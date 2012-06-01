using System.Collections.Generic;

namespace FubuMVC.Core.Diagnostics
{
    public interface IRequestHistoryCache
    {
        IEnumerable<IDebugReport> RecentReports();
    }
}