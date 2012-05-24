using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;
using FubuMVC.Diagnostics.Models;
using FubuMVC.Diagnostics.Models.Grids;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes
{
    public class get_Coumn_Value_handler
    {
        private readonly IModelBuilder<InstrumentationCacheModel> _modelBuilder;

        public get_Coumn_Value_handler(IModelBuilder<InstrumentationCacheModel> modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public InstrumentationCacheModel Execute(InstrumentationRouteRequestModel request)
        {
            var model = _modelBuilder.Build();
            model.Filter = new JsonGridFilter
                               {
                                   ColumnName = request.Column,
                                   Values = new[] {request.Value}
                               };

            return model;
        }
    }
}