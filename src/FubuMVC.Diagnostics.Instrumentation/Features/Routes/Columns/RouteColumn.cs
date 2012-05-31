using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.Columns
{
    public class RouteColumn : GridColumnBase<RouteInstrumentationModel>
    {
        public RouteColumn()
            : base("Route")
        {
        }

        public override int Rank()
        {
            return 100;
        }

        public override string ValueFor(RouteInstrumentationModel model)
        {
            if (model.Url == null)
            {
                return "N/A";
            }
            var pattern = model.Url;
            if (pattern == string.Empty)
            {
                pattern = "(default)";
            }
            return pattern;
        }
    }
}