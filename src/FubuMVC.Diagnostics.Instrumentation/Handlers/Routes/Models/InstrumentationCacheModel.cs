using System.Collections.Generic;
using FubuMVC.Diagnostics.Models;
using FubuMVC.Diagnostics.Models.Grids;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models
{
    public class InstrumentationCacheModel : IGridModel
    {
        public JqGridColumnModel ColumnModel { get; set; }
        public JsonGridFilter Filter { get; set; }
        public List<RouteInstrumentationModel> RouteInstrumentations { get; set; }
    }
}