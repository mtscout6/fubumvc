using System;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration.Conventions;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;

namespace FubuMVC.Diagnostics.Core.Configuration.Policies
{
    public class DiagnosticsHandlerUrlPolicy : HandlersUrlPolicy
    {
        public DiagnosticsHandlerUrlPolicy(params Type[] markerTypes)
            : base(markerTypes)
        {
        }

        public override bool Matches(ActionCall call, IConfigurationObserver log)
        {
            if(!IsDiagnosticsHandler(call))
            {
                return false;
            }

            return base.Matches(call, log);
        }

        protected virtual bool IsDiagnosticsHandler(ActionCall call)
        {
            return call.IsDiagnosticsHandler();
        }

        protected override void visit(IRouteDefinition routeDefinition)
        {
            routeDefinition.Append(DiagnosticsUrls.ROOT);
        }
    }
}