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
        private readonly ConcurrentDictionary<Guid, RouteInstrumentationReport> _instrumentationReports;

        public InstrumentationReportCache(BehaviorGraph graph)
        {
            _graph = graph;
            _instrumentationReports = new ConcurrentDictionary<Guid, RouteInstrumentationReport>();
        }

        public void AddReport(IDebugReport debugReport, CurrentRequest request)
        {
            var incrementValues = new Action<RouteInstrumentationReport>(report => 
            {
                var visitor = new RecordedRequestBehaviorVisitor();
                debugReport.Steps.Each(s => s.Details.AcceptVisitor(visitor));

                if(visitor.HasExceptions())
                {
                    report.IncrementExceptionCount();
                    report.RecordException(debugReport.BehaviorId, visitor.Exceptions());
                }

                report.IncrementHitCount();
                report.AddExecutionTime((long)debugReport.ExecutionTime);
            });
            _instrumentationReports.AddOrUpdate(debugReport.BehaviorId,
                guid =>
                {
                    RouteInstrumentationReport report;
                    var chain = _graph.Behaviors.SingleOrDefault(c => c.UniqueId == debugReport.BehaviorId);
                    if (chain != null && chain.Route != null)
                    {
                        report = new RouteInstrumentationReport(chain.Route.Pattern);
                    }
                    else
                    {
                        report = new RouteInstrumentationReport();
                    }

                    incrementValues(report);
                    return report;
                },
                (guid, report) =>
                {
                    incrementValues(report);
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
