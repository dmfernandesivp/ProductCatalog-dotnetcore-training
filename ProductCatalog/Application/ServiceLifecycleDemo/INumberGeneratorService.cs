using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceLifecycleDemo
{
    public interface INumberGeneratorService
    {
        int Number1 { get; }
        int Number2 { get; }
        string ServiceId { get; }
    }
}
