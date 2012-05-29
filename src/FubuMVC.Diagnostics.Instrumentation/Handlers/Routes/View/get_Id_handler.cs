using System.Linq;
using System.Collections.Generic;
using FubuMVC.Diagnostics.Features.Requests;
using FubuMVC.Diagnostics.Instrumentation.Diagnostics;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.View
{
    public class get_Id_handler
    {
        private readonly InstrumentationReportCache _reportCache;

        public get_Id_handler(InstrumentationReportCache reportCache)
        {
            _reportCache = reportCache;
        }

        public InstrumentationDetailsModel Execute(InstrumentationInputModel inputModel)
        {
            var report = _reportCache.GetReport(inputModel.Id);
            var model = new InstrumentationDetailsModel
            {
                Id = report.BehaviorId,
                Url = report.Route,
                AverageExecution = report.AverageExecutionTime,
                ExceptionCount = report.ExceptionCount,
                HitCount = report.HitCount,
                MaxExecution = report.MaxExecutionTime,
                MinExecution = report.MinExecutionTime
            };
            model.RequestOverviews.AddRange(report.Reports
                .OrderByDescending(x => x.Time)
                .Select(x =>
                {
                    var visitor = new RecordedRequestBehaviorVisitor();
                    x.Steps.Each(s => s.Details.AcceptVisitor(visitor));
                    return new InstrumentationRequestOverviewModel
                    {
                        DateTime = x.Time.ToString(),
                        ExecutionTime = x.ExecutionTime.ToString(),
                        HasException = visitor.HasExceptions()
                    };
                }));

            return model;
        }
    }
}
