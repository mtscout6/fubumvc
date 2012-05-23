using FubuMVC.Core;
using FubuMVC.Diagnostics.Core.Configuration.Policies;
using FubuMVC.Diagnostics.Core.Grids;
using FubuMVC.Diagnostics.Instrumentation.Diagnostics;
using FubuMVC.Diagnostics.Instrumentation.Models;
using FubuMVC.Spark;

namespace FubuMVC.Diagnostics.Instrumentation
{
    public class InstrumentationDiagnosticsRegistry : FubuPackageRegistry
    {
        public InstrumentationDiagnosticsRegistry()
        {
            Applies
                .ToAssemblyContainingType<InstrumentationDiagnosticsRegistry>();

            ApplyHandlerConventions(markers => new DiagnosticsHandlerUrlPolicy(markers),
                                    typeof (InstrumentationDiagnosticsRegistry));
            
            Views
                .TryToAttachWithDefaultConventions();

            Routes
                .UrlPolicy<DiagnosticsAttributeUrlPolicy>();

            Services(x =>
            {
                x.SetServiceIfNone<IInstrumentationReportCache, InstrumentationReportCache>();
                x.SetServiceIfNone<IGridRowProvider<InstrumentationCacheModel, RouteInstrumentationModel>, InstrumentationCacheRowProvider>();
            });



            Import<SparkEngine>();
        }
    }
}