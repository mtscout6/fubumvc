using System.Linq;
using FubuMVC.Diagnostics.Instrumentation.Diagnostics;
using FubuMVC.Diagnostics.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Models
{
    public class InstrumentationCacheModelBuilder : IModelBuilder<InstrumentationCacheModel>
    {
        private readonly IInstrumentationReportCache _instrumentationCache;

        public InstrumentationCacheModelBuilder(IInstrumentationReportCache instrumentationCache)
        {
            _instrumentationCache = instrumentationCache;
        }

        public InstrumentationCacheModel Build()
        {
            return new InstrumentationCacheModel
            {
                RouteInstrumentations = _instrumentationCache.Select(r => new RouteInstrumentationModel
                {
                    Url = r.Url,
                    HitCount = r.HitCount
                }).ToList()
            };
        }
    }
}