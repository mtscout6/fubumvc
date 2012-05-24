using System.Collections.Generic;
using FubuMVC.Diagnostics.Core.Grids;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes
{
    public class InstrumentationCacheRowProvider : IGridRowProvider<InstrumentationCacheModel, RouteInstrumentationModel>
    {
        public IEnumerable<RouteInstrumentationModel> RowsFor(InstrumentationCacheModel target)
        {
            return target.RouteInstrumentations;
        }
    }
}