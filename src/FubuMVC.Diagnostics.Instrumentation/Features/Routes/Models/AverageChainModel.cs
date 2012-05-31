using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration.Nodes;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models
{
    public class AverageChainModel
    {
        public AverageChainModel()
        {
            BehaviorAverages = Enumerable.Empty<AverageBehaviorModel>();
        }

        public string Constraints { get; set; }
        public BehaviorChain Chain { get; set; }
        public IEnumerable<AverageBehaviorModel> BehaviorAverages { get; set; }
    }
}