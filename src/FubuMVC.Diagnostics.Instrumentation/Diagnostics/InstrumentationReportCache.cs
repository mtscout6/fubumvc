using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using FubuMVC.Core;
using FubuMVC.Core.Diagnostics;

namespace FubuMVC.Diagnostics.Instrumentation.Diagnostics
{
    public interface IInstrumentationReportCache : IEnumerable<RouteInstrumentationReport>
    {
    }

    public class InstrumentationReportCache : IInstrumentationReportCache
    {
        private readonly ConcurrentDictionary<Guid, RouteInstrumentationReport> _instrumentationReports;

        public InstrumentationReportCache(IDebugReportDistributer distributer)
        {
            _instrumentationReports = new ConcurrentDictionary<Guid, RouteInstrumentationReport>();
            distributer.Register(AddReport);
        }

        private void AddReport(IDebugReport debugReport, CurrentRequest request)
        {
            _instrumentationReports.AddOrUpdate(debugReport.BehaviorId,
                guid =>
                {
                    debugReport.RecordFormData();
                    var report = new RouteInstrumentationReport(debugReport.Url);
                    report.IncrementHitCount();
                    return report;
                },
                (guid, report) =>
                {
                    report.IncrementHitCount();
                    return report;
                });
        }

        public IEnumerator<RouteInstrumentationReport> GetEnumerator()
        {
            return _instrumentationReports.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}