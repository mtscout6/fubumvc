namespace FubuMVC.Core.Diagnostics
{
    public interface IDebugReportDistributer
    {
        void Publish(IDebugReport report, CurrentRequest request);
    }
}