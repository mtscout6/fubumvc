using FubuMVC.Diagnostics.Core.Grids;
using FubuMVC.Diagnostics.Instrumentation.Models;
using FubuMVC.Diagnostics.Models;
using FubuMVC.Diagnostics.Models.Grids;

namespace FubuMVC.Diagnostics.Instrumentation.Filter
{
    public class PostHandler
    {
        private readonly IGridService<InstrumentationCacheModel, RouteInstrumentationModel> _gridService;
        private readonly IModelBuilder<InstrumentationCacheModel> _modelBuilder;

        public PostHandler(IGridService<InstrumentationCacheModel, RouteInstrumentationModel> gridService, IModelBuilder<InstrumentationCacheModel> modelBuilder)
        {
            _modelBuilder = modelBuilder;
            _gridService = gridService;
        }

        public JsonGridModel Execute(JsonGridQuery<InstrumentationCacheModel> query)
        {
            var model = _modelBuilder.Build();
            return _gridService.GridFor(model, query);
        }
    }
}