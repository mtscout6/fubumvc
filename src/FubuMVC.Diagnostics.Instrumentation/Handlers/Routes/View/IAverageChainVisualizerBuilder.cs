using System;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.View
{
    public interface IAverageChainVisualizerBuilder
    {
        AverageChainModel VisualizerFor(Guid uniqueId);
    }
}