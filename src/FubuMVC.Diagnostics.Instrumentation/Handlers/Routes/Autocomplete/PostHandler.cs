using System;
using System.Linq;
using System.Collections.Generic;
using FubuMVC.Diagnostics.Core.Grids;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;
using FubuMVC.Diagnostics.Models;
using FubuMVC.Diagnostics.Models.Grids;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Autocomplete
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

        public JsonAutocompleteResultModel Execute(AutocompleteRequestModel<InstrumentationCacheModel> request)
        {
            var model = _modelBuilder.Build();
            var filter = new JsonGridFilter { ColumnName = request.Column, Values = new List<string> { request.Query } };
            var query = JsonGridQuery.ForFilter(filter);
            return new JsonAutocompleteResultModel
            {
                Values = _gridService
                    .GridFor(model, query)
                    .Rows
                    .SelectMany(r => r.Columns.Where(c => c.Name.Equals(request.Column, StringComparison.OrdinalIgnoreCase)))
                    .Distinct()
            };
        }
    }
}