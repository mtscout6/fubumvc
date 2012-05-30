using System;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models
{
    public class InstrumentationRequestOverviewModel
    {
        public Guid Id { get; set; }
        public string DateTime { get; set; }
        public string ExecutionTime { get; set; }
        public bool HasException { get; set; }
    }
}