using Application.ServiceLifecycleDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceLifecycleDemo
{
    public interface IApplicationService
    {
        LifecycleComparisonResponse GetLifecycleComparison();
    }

    public class ApplicationService : IApplicationService
    {
        private readonly SingletonNumberService _singletonService;
        private readonly ScopedNumberService _scopedService1;
        private readonly ScopedNumberService _scopedService2;
        private readonly TransientNumberService _transientService1;
        private readonly TransientNumberService _transientService2;

        public ApplicationService(
            SingletonNumberService singletonService,
            ScopedNumberService scopedService1,
            ScopedNumberService scopedService2,
            TransientNumberService transientService1,
            TransientNumberService transientService2)
        {
            _singletonService = singletonService;
            _scopedService1 = scopedService1;
            _scopedService2 = scopedService2;
            _transientService1 = transientService1;
            _transientService2 = transientService2;
        }

        public LifecycleComparisonResponse GetLifecycleComparison()
        {
            return new LifecycleComparisonResponse
            {
                Singleton = new ServiceResponse
                {
                    Number1 = _singletonService.Number1,
                    Number2 = _singletonService.Number2,
                    ServiceId = _singletonService.ServiceId
                },
                Scoped1 = new ServiceResponse
                {
                    Number1 = _scopedService1.Number1,
                    Number2 = _scopedService1.Number2,
                    ServiceId = _scopedService1.ServiceId
                },
                Scoped2 = new ServiceResponse
                {
                    Number1 = _scopedService2.Number1,
                    Number2 = _scopedService2.Number2,
                    ServiceId = _scopedService2.ServiceId
                },
                Transient1 = new ServiceResponse
                {
                    Number1 = _transientService1.Number1,
                    Number2 = _transientService1.Number2,
                    ServiceId = _transientService1.ServiceId
                },
                Transient2 = new ServiceResponse
                {
                    Number1 = _transientService2.Number1,
                    Number2 = _transientService2.Number2,
                    ServiceId = _transientService2.ServiceId
                },
                RequestId = Guid.NewGuid().ToString("N")[..8]
            };
        }
    }
}
