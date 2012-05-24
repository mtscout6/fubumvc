using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns
{
    public class MaxExecutionColumn : GridColumnBase<RouteInstrumentationModel>
    {
        public MaxExecutionColumn()
            : base("Max Execution Time (ms)")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return target.MaxExecution.ToString();
        }

        public override int Rank()
        {
            return 4;
        }
    }
}