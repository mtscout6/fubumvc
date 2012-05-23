using FubuMVC.Core;
using FubuMVC.Diagnostics.Core.Configuration.Policies;
using FubuMVC.Diagnostics.Instrumentation.Diagnostics;
using FubuMVC.Spark;

namespace FubuMVC.Diagnostics.Instrumentation
{
    public class PerformanceDiagnosticsRegistry : FubuPackageRegistry
    {
        public PerformanceDiagnosticsRegistry()
        {
            Applies
                .ToAssemblyContainingType<PerformanceDiagnosticsRegistry>();

            ApplyHandlerConventions(markers => new DiagnosticsHandlerUrlPolicy(markers),
                                    typeof (PerformanceDiagnosticsRegistry));

            Routes
                .UrlPolicy<DiagnosticsAttributeUrlPolicy>();

            Services(x => x.SetServiceIfNone<IInstrumentationReportCache, InstrumentationReportCache>());

            Import<SparkEngine>();
        }
    }
}