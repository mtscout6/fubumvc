using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Columns
{
    public class Url : GridColumnBase<RouteInstrumentationModel>
    {
        public Url() 
            : base("Url")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return target.Url;
        }

        public override bool IsIdentifier()
        {
            return true;
        }
    }
}