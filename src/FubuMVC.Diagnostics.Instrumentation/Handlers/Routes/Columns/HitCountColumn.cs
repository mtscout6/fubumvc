﻿using FubuMVC.Diagnostics.Core.Grids.Columns;
using FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Models;

namespace FubuMVC.Diagnostics.Instrumentation.Handlers.Routes.Columns
{
    public class HitCountColumn : GridColumnBase<RouteInstrumentationModel>
    {
        public HitCountColumn() 
            : base("Hit Count")
        {
        }

        public override string ValueFor(RouteInstrumentationModel target)
        {
            return target.HitCount.ToString();
        }

        public override int Rank()
        {
            return 50;
        }
    }
}