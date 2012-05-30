using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns
{
    public class MinExecutionColumn : GridColumnBase<RouteInstrumentationModel>
    {
        public MinExecutionColumn()
            : base("Min Execution Time (ms)")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return target.MinExecution.ToString();
        }

        public override int Rank()
        {
            return 5;
        }
    }
}