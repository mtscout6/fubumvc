using FubuMVC.Diagnostics.Instrumentation.Models;
using FubuMVC.Diagnostics.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers
{
    public class GetHandler
    {
        private readonly IModelBuilder<InstrumentationCacheModel> _modelBuilder;

        public GetHandler(IModelBuilder<InstrumentationCacheModel> modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        [FubuDiagnosticsUrl("~/instrumentation")]
        public InstrumentationCacheModel Execute(InstrumentationRequestModel inputModel)
        {
            return _modelBuilder.Build();
        }
    }
}