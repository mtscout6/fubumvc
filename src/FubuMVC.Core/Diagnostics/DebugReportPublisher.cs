using System;
using System.Collections.Concurrent;
using FubuMVC.Core.Registration;
using System.Collections.Generic;

namespace FubuMVC.Core.Diagnostics
{
    [Singleton]
    public class DebugReportPublisher : IDebugReportPublisher
    {
        private readonly ConcurrentBag<Action<IDebugReport, CurrentRequest>> _consumers;

        public DebugReportPublisher()
        {
            _consumers = new ConcurrentBag<Action<IDebugReport, CurrentRequest>>();
        }

        public void Publish(IDebugReport report, CurrentRequest request)
        {
            _consumers.Each(c => c(report, request));
        }

        public void Register(Action<IDebugReport, CurrentRequest> action)
        {
            _consumers.Add(action);
        }
    }
}