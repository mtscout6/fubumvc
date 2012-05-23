using FubuMVC.Core;
using FubuMVC.Core.Registration;
using FubuMVC.Diagnostics.Features.Performance;

namespace FubuMVC.Diagnostics.Navigation
{
    public class PerformanceExplorerAction : NavigationItemBase
    {
        public PerformanceExplorerAction(BehaviorGraph graph, IEndpointService endpointService)
            : base(graph, endpointService)
        {
        }

        public override string Text()
        {
            return "Performance";
        }

        protected override object inputModel()
        {
            return new PerformanceRequestModel();
        }
    }
}