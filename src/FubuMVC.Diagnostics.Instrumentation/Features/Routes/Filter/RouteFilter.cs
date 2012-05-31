using FubuMVC.Diagnostics.Core.Grids.Filters;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Columns;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.Filter
{
    public class RouteFilter : GridFilterBase<RouteColumn, RouteInstrumentationModel>
    {
        public RouteFilter(RouteColumn column)
            : base(column)
        {
        }
    }
}