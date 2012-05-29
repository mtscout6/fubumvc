using System.Linq;
using FubuMVC.Diagnostics.Instrumentation.Diagnostics;
using FubuMVC.Diagnostics.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models
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
                    Id = r.BehaviorId,
                    Url = r.Route,
                    HitCount = r.HitCount,
                    AverageExecution = r.AverageExecutionTime,
                    MaxExecution = r.MaxExecutionTime,
                    MinExecution = r.MinExecutionTime,
                    ExceptionCount = r.ExceptionCount
                }).ToList()
            };
        }
    }
}