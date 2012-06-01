using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Core.Diagnostics.Tracing
{
    public class DiagnosticBehavior : IActionBehavior
    {
        private readonly IDebugDetector _detector;
        private readonly IDebugReportPublisher _publisher;
        private readonly IDebugReport _report;
        private readonly IDebugCallHandler _debugCallHandler;
        private readonly IFubuRequest _request;

        public DiagnosticBehavior(IDebugReport report, IDebugDetector detector, IDebugReportPublisher publisher, IDebugCallHandler debugCallHandler, IFubuRequest request)
        {
            _report = report;
            _debugCallHandler = debugCallHandler;
            _request = request;
            _detector = detector;
            _publisher = publisher;
        }

        public IActionBehavior Inner { get; set; }

        public void Invoke()
        {
            try
            {
                _report.RecordFormData();
                Inner.Invoke();

                write();
            }
            finally
            {
                _publisher.Publish(_report, _request.Get<CurrentRequest>());
            }
        }

        public void InvokePartial()
        {
            Inner.InvokePartial();
        }

        private void write()
        {
            _report.MarkFinished();

            if (!_detector.IsDebugCall()) return;

            _detector.UnlatchWriting();

            _debugCallHandler.Handle();
        }
    }

}