using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Diagnostics.Instrumentation.Handlers;
using FubuMVC.Diagnostics.Instrumentation.Models;
using FubuMVC.Diagnostics.Navigation;

namespace FubuMVC.Diagnostics.Instrumentation.Navigation
{
    public class InstrumentationExplorerAction : NavigationItemBase
    {
        public InstrumentationExplorerAction(BehaviorGraph graph, IEndpointService endpointService)
            : base(graph, endpointService)
        {
        }

        public override string Text()
        {
            return "Instrumentation";
        }

        protected override object inputModel()
        {
            return new InstrumentationRequestModel();
        }
    }
}