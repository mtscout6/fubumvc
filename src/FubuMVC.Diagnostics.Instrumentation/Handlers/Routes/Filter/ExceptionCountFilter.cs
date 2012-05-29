using FubuMVC.Diagnostics.Core.Grids.Filters;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Filter
{
    public class ExceptionCountFilter : GridFilterBase<ExceptionCountColumn, RouteInstrumentationModel>
    {
        public ExceptionCountFilter(ExceptionCountColumn column)
            : base(column)
        {
        }
    }
}