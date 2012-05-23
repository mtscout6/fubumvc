using FubuMVC.Core;
using FubuMVC.Core.Diagnostics;
using FubuTestingSupport;
using NUnit.Framework;
using System.Linq;
using Rhino.Mocks;

namespace FubuMVC.Tests.Diagnostics
{
    [TestFixture]
    public class RequestHistoryServiceTester : InteractionContext<RequestHistoryCache>
    {
        private CurrentRequest theCurrentRequest;
        private DiagnosticsConfiguration _configuration;
        private IDebugReportDistributer _distributer;

        protected override void beforeEach()
        {
            theCurrentRequest = new CurrentRequest();

            _configuration = new DiagnosticsConfiguration {MaxRequests = 60};
            _distributer = new DebugReportDistributer();

            Container.Inject(theCurrentRequest);
            Container.Inject(_configuration);
            Container.Inject(_distributer);
        }

        [Test]
        public void only_keeps_maximum_records()
        {
            MockFor<IRequestHistoryCacheFilter>()
                .Expect(c => c.Exclude(theCurrentRequest))
                .Return(false)
                .Repeat
                .Any();

            // Just to trigger the constructor
            var classUnderTest = ClassUnderTest;

            for (int i = 0; i < _configuration.MaxRequests + 10; ++i)
            {
                _distributer.Publish(new DebugReport(null, null), theCurrentRequest);
            }

            ClassUnderTest
                .RecentReports()
                .ShouldHaveCount(_configuration.MaxRequests);
        }

        [Test]
        public void keep_the_newest_reports()
        {
            MockFor<IRequestHistoryCacheFilter>()
                .Expect(c => c.Exclude(theCurrentRequest))
                .Return(false)
                .Repeat
                .Any();

            // Just to trigger the constructor
            var classUnderTest = ClassUnderTest;

            for (int i = 0; i < _configuration.MaxRequests; i++)
            {
                _distributer.Publish(new DebugReport(null, null), theCurrentRequest);
            }

            var report1 = new DebugReport(null, null);
            var report2 = new DebugReport(null, null);
            var report3 = new DebugReport(null, null);

            _distributer.Publish(report1, theCurrentRequest);
            _distributer.Publish(report2, theCurrentRequest);
            _distributer.Publish(report3, theCurrentRequest);

            ClassUnderTest
                .RecentReports()
                .Take(3)
                .ShouldHaveTheSameElementsAs(report3, report2, report1);
        }

        [Test]
        public void should_not_add_report_if_any_filter_excludes()
        {
            MockFor<IRequestHistoryCacheFilter>()
                .Expect(c => c.Exclude(theCurrentRequest))
                .Return(true);

            // Just to trigger the constructor
            var classUnderTest = ClassUnderTest;

            _distributer.Publish(new DebugReport(null, null), theCurrentRequest);

            ClassUnderTest
                .RecentReports()
                .ShouldHaveCount(0);
        }
    }
}