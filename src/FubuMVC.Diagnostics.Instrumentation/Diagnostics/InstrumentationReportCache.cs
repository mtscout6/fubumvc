﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core;
using FubuMVC.Core.Diagnostics;
using FubuMVC.Core.Registration;
using FubuMVC.Diagnostics.Features.Requests;

namespace FubuMVC.Diagnostics.Instrumentation.Diagnostics
{
    public interface IInstrumentationReportCache : IEnumerable<RouteInstrumentationReport>, IDebugReportConsumer
    {
    }

    public class InstrumentationReportCache : IInstrumentationReportCache
    {
        private readonly BehaviorGraph _graph;
        private readonly DiagnosticsConfiguration _configuration;
        private readonly ConcurrentDictionary<Guid, RouteInstrumentationReport> _instrumentationReports;

        public InstrumentationReportCache(BehaviorGraph graph, DiagnosticsConfiguration configuration)
        {
            _graph = graph;
            _configuration = configuration;
            _instrumentationReports = new ConcurrentDictionary<Guid, RouteInstrumentationReport>();
        }

        public void AddReport(IDebugReport debugReport, CurrentRequest request)
        {
            _instrumentationReports.AddOrUpdate(debugReport.BehaviorId,
                guid =>
                {
                    RouteInstrumentationReport report;
                    var chain = _graph.Behaviors.SingleOrDefault(c => c.UniqueId == debugReport.BehaviorId);
                    if (chain != null && chain.Route != null)
                    {
                        report = new RouteInstrumentationReport(debugReport.BehaviorId, chain.Route.Pattern);
                    }
                    else
                    {
                        report = new RouteInstrumentationReport(debugReport.BehaviorId);
                    }

                    report.AddDebugReport(debugReport);
                    return report;
                },
                (guid, report) =>
                {
                    report.AddDebugReport(debugReport);
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
