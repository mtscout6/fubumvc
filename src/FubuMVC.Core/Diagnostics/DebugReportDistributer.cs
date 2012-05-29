using System;
using System.Collections.Generic;
using System.Threading;
using FubuMVC.Core.Registration;
using StructureMap;
using System.Linq;

namespace FubuMVC.Core.Diagnostics
{
    [Singleton]
    public class DebugReportDistributer : IDebugReportDistributer
    {
        private readonly IContainer _container;
        private bool _scanned;
        private int _scanners;
        private IList<Type> _consumerInterfaces;

        public DebugReportDistributer(IContainer container)
        {
            _container = container;
        }

        public void Publish(IDebugReport report, CurrentRequest request)
        {
            if (!_scanned)
            {
                var scannerCount = Interlocked.Increment(ref _scanners);

                if (scannerCount == 1)
                    ScanForConsumerInterfaces();
                else
                {
                    while(!_scanned)
                        Thread.SpinWait(100);
                }
            }

            foreach (var consumer in ResolveConsumers())
            {
                consumer.AddReport(report, request);
            }
        }

        private IEnumerable<IDebugReportConsumer> ResolveConsumers()
        {
            return _consumerInterfaces
               .SelectMany(derivedInterface => _container.GetAllInstances(derivedInterface).Cast<IDebugReportConsumer>())
               .Distinct();
        } 

        private void ScanForConsumerInterfaces()
        {
            var typePool = new TypePool(null);
            typePool.AddAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            _consumerInterfaces = typePool
                .TypesMatching(t => t.IsInterface && t.GetInterfaces().Contains(typeof (IDebugReportConsumer)))
                .ToList();

            _scanned = true;
        }
    }
}