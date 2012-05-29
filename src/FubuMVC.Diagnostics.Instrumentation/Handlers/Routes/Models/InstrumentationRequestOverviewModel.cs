namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models
{
    public class InstrumentationRequestOverviewModel
    {
        public string DateTime { get; set; }
        public string ExecutionTime { get; set; }
        public bool HasException { get; set; }
    }
}