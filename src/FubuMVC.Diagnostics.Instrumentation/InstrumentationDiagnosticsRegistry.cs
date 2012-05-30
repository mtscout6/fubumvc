using FubuMVC.Core;
using FubuMVC.Diagnostics.Core.Configuration.Policies;
using FubuMVC.Diagnostics.Core.Grids;
using FubuMVC.Diagnostics.Instrumentation.Diagnostics;
using FubuMVC.Diagnostics.Instrumentation.Handlers;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.View;
using FubuMVC.Spark;

namespace FubuMVC.Diagnostics.Instrumentation
{
    public class InstrumentationDiagnosticsRegistry : FubuPackageRegistry
    {
        public InstrumentationDiagnosticsRegistry()
        {
            Applies
                .ToAssemblyContainingType<InstrumentationDiagnosticsRegistry>();

            ApplyHandlerConventions(markers => new InstrumentationHandlerUrlPolicy(markers),
                                    typeof(InstrumentationHandlers));

            Views
                .TryToAttachWithDefaultConventions();

            Routes
                .UrlPolicy<DiagnosticsAttributeUrlPolicy>();
            
            Services(x =>
            {
                x.SetServiceIfNone<IInstrumentationReportCache, InstrumentationReportCache>();
                x.SetServiceIfNone<IGridRowProvider<InstrumentationCacheModel, RouteInstrumentationModel>, InstrumentationCacheRowProvider>();
                x.SetServiceIfNone<IAverageChainVisualizerBuilder, AverageChainVisualizerBuilder>();
            });

            Import<SparkEngine>();
        }
    }
}