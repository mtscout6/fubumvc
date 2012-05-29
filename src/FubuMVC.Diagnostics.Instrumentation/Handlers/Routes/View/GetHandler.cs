using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.View
{
    public class GetHandler
    {
        //[FubuDiagnosticsUrl("~/instrumentation/{Id}")]
        public InstrumentationDetailsModel Execute(InstrumentationInputModel inputModel)
        {
            return new InstrumentationDetailsModel();
        }
    }
}