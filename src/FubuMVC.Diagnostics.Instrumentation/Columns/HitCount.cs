using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Columns
{
    public class HitCount : GridColumnBase<RouteInstrumentationModel>
    {
        public HitCount() 
            : base("HitCount")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return target.HitCount.ToString();
        }
    }
}