using System;
using System.Collections.Concurrent;

namespace FubuMVC.Core.Diagnostics
{
    public class DebugReportDistributer : IDebugReportDistributer
    {
        private static readonly ConcurrentBag<Action<IDebugReport, CurrentRequest>> _actions = new ConcurrentBag<Action<IDebugReport, CurrentRequest>>();

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