using System.Collections.Generic;
using FubuMVC.Diagnostics.Instrumentation.Handlers;

namespace FubuMVC.Diagnostics.Instrumentation.Models
{
    public class InstrumentationCacheModel
    {
        public IEnumerable<RouteInstrumentationModel> RouteInstrumentations { get; set; }
    }
}