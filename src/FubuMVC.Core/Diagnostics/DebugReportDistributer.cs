using System;
using System.Collections.Generic;

namespace FubuMVC.Core.Diagnostics
{
    public class DebugReportDistributer : IDebugReportDistributer
    {
        private readonly List<Action<IDebugReport, CurrentRequest>> _actions = new List<Action<IDebugReport, CurrentRequest>>();

        public void Register(Action<IDebugReport, CurrentRequest> action)
        {
            _actions.Add(action);
        }

        public void Publish(IDebugReport report, CurrentRequest request)
        {
            var copies = _actions.ToArray();
            foreach (var action in copies)
            {
                action(report, request);
            }
        }
    }
}