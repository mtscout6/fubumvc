using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns
{
    public class FailureRateColumn : GridColumnBase<RouteInstrumentationModel>
    {
        public FailureRateColumn()
            : base("Failure Rate")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return ((target.ExceptionCount * 1.0/target.HitCount) * 100).ToString("F2") + "%";
        }

        public override int Rank()
        {
            return 40;
        }
    }
}