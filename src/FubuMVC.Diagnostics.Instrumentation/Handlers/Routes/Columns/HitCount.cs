using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns
{
    public class HitCount : GridColumnBase<RouteInstrumentationModel>
    {
        public HitCount() 
            : base("Hit Count")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return target.HitCount.ToString();
        }

        public override int Rank()
        {
            return 4;
        }
    }
}