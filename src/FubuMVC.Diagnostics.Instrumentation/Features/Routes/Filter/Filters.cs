using FubuMVC.Diagnostics.Core.Grids.Filters;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Columns;
using FubuMVC.Diagnostics.Instrumentation.Features.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Features.Routes.Filter
{
    public class Filters : GridFilterBase<AverageExecutionColumn, RouteInstrumentationModel>
    {
        public Filters(AverageExecutionColumn column)
            : base(column)
        {
        }
    }

    public class ExceptionCountFilter : GridFilterBase<ExceptionCountColumn, RouteInstrumentationModel>
    {
        public ExceptionCountFilter(ExceptionCountColumn column)
            : base(column)
        {
        }
    }

    public class HitCountFilter : GridFilterBase<HitCountColumn, RouteInstrumentationModel>
    {
        public HitCountFilter(HitCountColumn column)
            : base(column)
        {
        }
    }

    public class MaxExecutionFilter : GridFilterBase<MaxExecutionColumn, RouteInstrumentationModel>
    {
        public MaxExecutionFilter(MaxExecutionColumn column)
            : base(column)
        {
        }
    }

    public class MinExecutionFilter : GridFilterBase<MinExecutionColumn, RouteInstrumentationModel>
    {
        public MinExecutionFilter(MinExecutionColumn column)
            : base(column)
        {
        }
    }

    public class FailureRateFilter : GridFilterBase<MinExecutionColumn, RouteInstrumentationModel>
    {
        public FailureRateFilter(MinExecutionColumn column)
            : base(column)
        {
        }
    }
}