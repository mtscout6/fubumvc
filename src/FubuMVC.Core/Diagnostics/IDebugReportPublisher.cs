using System;

namespace FubuMVC.Core.Diagnostics
{
    public interface IDebugReportPublisher
    {
        void Publish(IDebugReport report, CurrentRequest request);
        void Register(Action<IDebugReport, CurrentRequest> action);
    }
}