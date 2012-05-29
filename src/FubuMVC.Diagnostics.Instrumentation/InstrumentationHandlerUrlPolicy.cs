using System;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Diagnostics.Core.Configuration.Policies;

namespace FubuMVC.Diagnostics.Instrumentation
{
    public class InstrumentationHandlerUrlPolicy : DiagnosticsHandlerUrlPolicy
    {
        public InstrumentationHandlerUrlPolicy(params Type[] markers)
            : base(markers)
        {
        }

        protected override bool IsDiagnosticsHandler(ActionCall call)
        {
            var instrumentationAssembly = GetType().Assembly;
            return IsHandlerCall(call)
                   && call.HandlerType.Assembly.Equals(instrumentationAssembly)
                   && !call.HasAttribute<FubuDiagnosticsUrlAttribute>();
        }

        protected override void visit(IRouteDefinition routeDefinition)
        {
            base.visit(routeDefinition);
            routeDefinition.Append("/instrumentation");
        }
    }
}