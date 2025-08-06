using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceLifecycleDemo.Models;

public class ServiceResponse
{
    public int Number1 { get; set; }
    public int Number2 { get; set; }
    public string ServiceId { get; set; } = string.Empty;
}
