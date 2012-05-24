using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns
{
    public class UrlColumn : GridColumnBase<RouteInstrumentationModel>
    {
        public UrlColumn() 
            : base("Url")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return target.Url;
        }

        public override int Rank()
        {
            return 5;
        }
    }
}