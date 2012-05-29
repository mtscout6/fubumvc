namespace FubuMVC.Core.Diagnostics
{
    public interface IDebugReportConsumer
    {
        void AddReport(IDebugReport report, CurrentRequest request);
    }
}