using System;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models
{
    public class InstrumentationRouteDetailsRequestModel
    {
        public Guid Id { get; set; }
        public Guid ReportId { get; set; }
    }
}