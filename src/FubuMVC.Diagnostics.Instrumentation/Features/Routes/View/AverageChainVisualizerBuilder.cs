using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Registration;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Diagnostics.Core.Infrastructure;
using FubuMVC.Diagnostics.Instrumentation.Diagnostics;
using FubuCore;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.View
{
    public class AverageChainVisualizerBuilder : IAverageChainVisualizerBuilder
    {
        private readonly BehaviorGraph _graph;
        private readonly IHttpConstraintResolver _constraintResolver;
        private readonly IInstrumentationReportCache _instrumentationReportCache;

        public AverageChainVisualizerBuilder(BehaviorGraph graph,
            IHttpConstraintResolver constraintResolver,
            IInstrumentationReportCache instrumentationReportCache)
        {
            _graph = graph;
            _constraintResolver = constraintResolver;
            _instrumentationReportCache = instrumentationReportCache;
        }

        public AverageChainModel VisualizerFor(Guid uniqueId)
        {
            var chain = _graph
                .Behaviors
                .SingleOrDefault(c => c.UniqueId == uniqueId);

            if (chain == null)
            {
                return null;
            }

            return new AverageChainModel
            {
                Chain = chain,
                Constraints = _constraintResolver.Resolve(chain),
                BehaviorAverages = BuildBehaviorAverages(uniqueId, chain)
            };
        }

        private IEnumerable<AverageBehaviorModel> BuildBehaviorAverages(Guid uniqueId, BehaviorChain chain)
        {
            var keyedAverages = new Dictionary<Guid, AverageBehaviorModel>();
            var averages = chain.Select(c =>
            {
                var behavior = new AverageBehaviorModel
                {
                    Id = c.UniqueId,
                    DisplayType = c.GetType().PrettyPrint(),
                    BehaviorType = c.ToString()
                };

                keyedAverages.Add(c.UniqueId, behavior);
                return behavior;
            }).ToList();

            _instrumentationReportCache.GetReport(uniqueId).Reports.Each(
                debugReport => debugReport.Each(behaviorReport =>
                {
                    var model = keyedAverages[behaviorReport.BehaviorId];

                    model.HitCount++;
                    model.TotalExecutionTime += behaviorReport.ExecutionTime;
                }));

            return averages;
        }
    }
}