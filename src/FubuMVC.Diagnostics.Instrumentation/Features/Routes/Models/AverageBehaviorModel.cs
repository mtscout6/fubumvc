using System;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models
{
    public class AverageBehaviorModel
    {
        public Guid Id { get; set; }
        public string BehaviorType { get; set; }
        public string DisplayType { get; set; }

        public int HitCount { get; set; }
        public double AverageExecutionTime { get { return TotalExecutionTime / HitCount; } }
        public double TotalExecutionTime { get; set; }
    }
}