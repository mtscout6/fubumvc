using System;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.View
{
    public interface IAverageChainVisualizerBuilder
    {
        AverageChainModel VisualizerFor(Guid uniqueId);
    }
}