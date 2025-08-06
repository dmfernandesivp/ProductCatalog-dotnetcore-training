using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceLifecycleDemo
{
    public class TransientNumberService : INumberGeneratorService
    {
        public int Number1 { get; }
        public int Number2 { get; }
        public string ServiceId { get; }

        public TransientNumberService()
        {
            var random = new Random();
            Number1 = random.Next(1, 100);
            Number2 = random.Next(1, 100);
            ServiceId = Guid.NewGuid().ToString("N")[..8];
        }
    }
}
