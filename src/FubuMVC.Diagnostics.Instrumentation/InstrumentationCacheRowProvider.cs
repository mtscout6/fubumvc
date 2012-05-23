using System.Collections.Generic;
using FubuMVC.Diagnostics.Instrumentation.Models;

namespace FubuMVC.Diagnostics.Core.Grids
{
    public class InstrumentationCacheRowProvider : IGridRowProvider<InstrumentationCacheModel, RouteInstrumentationModel>
    {
        public IEnumerable<RouteInstrumentationModel> RowsFor(InstrumentationCacheModel target)
        {
            return target.RouteInstrumentations;
        }
    }
}