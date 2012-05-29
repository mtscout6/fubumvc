using System;
using System.Collections.Generic;
using FubuMVC.Core.Diagnostics;

namespace FubuMVC.Core
{
    public class DiagnosticsConfiguration
    {
        public int MaxRequests { get; set; }
    }

    public interface IDiagnosticsConfigurationExpression
    {
        void LimitRecordingTo(int nrRequests);
        void ExcludeRequests(ICacheFilter filter);
    }

    public static class DiagnosticsConfigurationExtensions
    {
        public static void ApplyFilter<TFilter>(this IDiagnosticsConfigurationExpression config)
            where TFilter : ICacheFilter, new()
        {
            config.ExcludeRequests(new TFilter());
        }

        public static void ExcludeRequests(this IDiagnosticsConfigurationExpression config,
                                           Func<CurrentRequest, bool> shouldExclude)
        {
            config.ExcludeRequests(new LambdaCacheFilter(shouldExclude));
        }
    }

    public class DiagnosticsConfigurationExpression : IDiagnosticsConfigurationExpression
    {
        private readonly IList<ICacheFilter> _filters;

        public DiagnosticsConfigurationExpression(IList<ICacheFilter> filters)
        {
            _filters = filters;
        }

        public int MaxRequests { get; private set; }

        public void LimitRecordingTo(int nrRequests)
        {
            MaxRequests = nrRequests;
        }

        public void ExcludeRequests(ICacheFilter filter)
        {
            _filters.Fill(filter);
        }
    }
}