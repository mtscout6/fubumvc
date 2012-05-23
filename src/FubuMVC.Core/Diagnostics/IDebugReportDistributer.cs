using System;

namespace FubuMVC.Core.Diagnostics
{
    public interface IDebugReportDistributer
    {
        void Register(Action<IDebugReport, CurrentRequest> action);
        void Publish(IDebugReport report, CurrentRequest request);
    }
}