namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models
{
    public class RouteInstrumentationModel
    {
        public string Url { get; set; }
        public long HitCount { get; set; }
        public decimal AverageExecution { get; set; }
        public long MinExecution { get; set; }
        public long MaxExecution { get; set; }
        public long ExceptionCount { get; set; }
    }
}