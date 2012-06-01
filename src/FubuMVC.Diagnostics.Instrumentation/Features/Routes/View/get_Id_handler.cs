using System;
using System.Linq;
using System.Collections.Generic;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Diagnostics.Features.Requests;
using FubuMVC.Diagnostics.Instrumentation.Diagnostics;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.View
{
    public class get_Id_handler
    {
        private readonly IAverageChainVisualizerBuilder _averageChainVisualizerBuilder;
        private readonly IInstrumentationReportCache _reportCache;

        public get_Id_handler(IInstrumentationReportCache reportCache, IAverageChainVisualizerBuilder averageChainVisualizerBuilder)
        {
            _reportCache = reportCache;
            _averageChainVisualizerBuilder = averageChainVisualizerBuilder;
        }

        public InstrumentationDetailsModel Execute(InstrumentationInputModel inputModel)
        {
            var report = _reportCache.GetReport(inputModel.Id);

            if (report == null)
                return new InstrumentationDetailsModel{Id = Guid.Empty};

            var model = new InstrumentationDetailsModel
            {
                Id = report.BehaviorId,
                Url = report.Route,
                AverageExecution = report.AverageExecutionTime,
                ExceptionCount = report.ExceptionCount,
                HitCount = report.HitCount,
                MaxExecution = report.MaxExecutionTime,
                MinExecution = report.MinExecutionTime,
                AverageChain = _averageChainVisualizerBuilder.VisualizerFor(inputModel.Id)
            };
            model.RequestOverviews.AddRange(report.Reports
                .OrderByDescending(x => x.Time)
                .Select(x =>
                {
                    var visitor = new RecordedRequestBehaviorVisitor();
                    x.Steps.Each(s => s.Details.AcceptVisitor(visitor));
                    return new InstrumentationRequestOverviewModel
                    {
                        Id = x.Id,
                        DateTime = x.Time.ToString(),
                        ExecutionTime = x.ExecutionTime.ToString(),
                        HasException = visitor.HasExceptions(),
                        IsWarning = IsWarning(model, x)
                    };
                }));

            return model;
        }

        private bool IsWarning(InstrumentationDetailsModel model, IDebugReport report)
        {
            var max = model.MaxExecution;
            var avg = model.AverageExecution;
            var p1 = 1 - (double)report.ExecutionTime / max;
            var p2 = 1- (double)avg / max;
            return (p2 - p1) > 0.25;
        }
    }
}
