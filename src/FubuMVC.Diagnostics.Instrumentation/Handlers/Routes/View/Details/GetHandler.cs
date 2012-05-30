using System.Collections.Generic;
using System.Linq;
using FubuMVC.Diagnostics.Instrumentation.Diagnostics;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.View.Details
{
    public class GetHandler
    {
        private readonly IInstrumentationReportCache _reportCache;

        public GetHandler(IInstrumentationReportCache reportCache)
        {
            _reportCache = reportCache;
        }

        public InstrumentationRouteDetailsModel Execute(InstrumentationRouteDetailsRequestModel inputModel)
        {
            var model = new InstrumentationRouteDetailsModel();
            var report = _reportCache.GetReport(inputModel.Id);
            var debugReport = report.Reports.FirstOrDefault(r => r.Id == inputModel.ReportId);
            if (debugReport != null)
            {
                debugReport.Each(x => model.Steps.Add(new BehaviorDetailModel(x)));
            }
            return model;
        }
    }
}