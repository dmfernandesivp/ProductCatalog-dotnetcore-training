using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceLifecycleDemo.Models
{
    public class LifecycleComparisonResponse
    {
        public ServiceResponse Singleton { get; set; } = new();
        public ServiceResponse Scoped1 { get; set; } = new();
        public ServiceResponse Scoped2 { get; set; } = new();
        public ServiceResponse Transient1 { get; set; } = new();
        public ServiceResponse Transient2 { get; set; } = new();
        public string RequestId { get; set; } = string.Empty;
    }
}
