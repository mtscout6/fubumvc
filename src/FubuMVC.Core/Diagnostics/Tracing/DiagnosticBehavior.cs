using System;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;

namespace FubuMVC.Core.Diagnostics.Tracing
{
    public class DiagnosticBehavior : IActionBehavior
    {
        private readonly IDebugDetector _detector;
        private readonly IDebugReport _report;
        private readonly IDebugCallHandler _debugCallHandler;
        private readonly Action _diagnostics;

        public DiagnosticBehavior(IDebugReport report, IDebugDetector detector, IDebugReportDistributer distributer, IDebugCallHandler debugCallHandler, IFubuRequest request)
        {
            _report = report;
            _debugCallHandler = debugCallHandler;
            _detector = detector;

            _diagnostics = () => distributer.Publish(report, request.Get<CurrentRequest>());
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
                _diagnostics();
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