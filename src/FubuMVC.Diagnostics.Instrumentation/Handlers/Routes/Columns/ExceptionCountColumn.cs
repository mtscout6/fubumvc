using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns
{
    public class ExceptionCountColumn : GridColumnBase<RouteInstrumentationModel>
    {
        public ExceptionCountColumn() 
            : base("Exception Count")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return target.ExceptionCount.ToString();
        }

        public override int Rank()
        {
            return 45;
        }
    }
}