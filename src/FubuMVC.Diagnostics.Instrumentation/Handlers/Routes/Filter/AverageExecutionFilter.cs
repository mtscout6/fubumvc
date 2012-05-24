using FubuMVC.Diagnostics.Core.Grids.Filters;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Filter
{
    public class AverageExecutionFilter : GridFilterBase<AverageExecutionColumn, RouteInstrumentationModel>
    {
        public AverageExecutionFilter(AverageExecutionColumn column)
            : base(column)
        {
        }
    }
}