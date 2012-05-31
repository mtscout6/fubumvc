using System.Collections.Generic;
using System.Linq;
using FubuMVC.Diagnostics.Core.Grids;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models;
using FubuMVC.Diagnostics.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes
{
    public class GetHandler
    {
        private readonly IModelBuilder<InstrumentationCacheModel> _modelBuilder;
        private readonly IEnumerable<IGridColumnBuilder<RouteInstrumentationModel>> _columnBuilders;

        public GetHandler(IModelBuilder<InstrumentationCacheModel> modelBuilder, IEnumerable<IGridColumnBuilder<RouteInstrumentationModel>> columnBuilders)
        {
            _modelBuilder = modelBuilder;
            _columnBuilders = columnBuilders;
        }

        public InstrumentationCacheModel Execute(InstrumentationRequestModel inputModel)
        {
            var report =  _modelBuilder.Build();

            var columnModel = new JqGridColumnModel();
            var behaviorReport = report.RouteInstrumentations.FirstOrDefault();

            if (behaviorReport != null)
            {
                _columnBuilders
                    .SelectMany(builder => builder.ColumnsFor(behaviorReport))
                    .Each(col => columnModel.AddColumn(new JqGridColumn
                    {
                        hidden = col.IsHidden,
                        hidedlg = col.IsHidden,
                        hideFilter = col.HideFilter,
                        name = col.Name,
                        index = col.Name
                    }));
            }

            return new InstrumentationCacheModel { ColumnModel = columnModel };
        }
    }
}