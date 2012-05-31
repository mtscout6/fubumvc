using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.Columns
{
    public class AverageExecutionColumn : GridColumnBase<RouteInstrumentationModel>
    {
        public AverageExecutionColumn()
            : base("Average Execution Time (ms)")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return target.AverageExecution.ToString("F2");
        }

        public override int Rank()
        {
            return 35;
        }
    }
}