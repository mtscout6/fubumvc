using FubuMVC.Diagnostics.Instrumentation.Models;
using FubuMVC.Diagnostics.Models;
using FubuMVC.Diagnostics.Models.Grids;

namespace FubuMVC.Diagnostics.Features.Routes
{
    public class get_Do_Something_handler
    {
        private readonly IModelBuilder<InstrumentationCacheModel> _modelBuilder;

        public get_Do_Something_handler(IModelBuilder<InstrumentationCacheModel> modelBuilder)
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