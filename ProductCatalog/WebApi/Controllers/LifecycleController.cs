using Application.ServiceLifecycleDemo.Models;
using Application.ServiceLifecycleDemo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifecycleController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public LifecycleController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Demonstrates the difference between Singleton, Scoped, and Transient service lifecycles
        /// </summary>
        [HttpGet("compare")]
        public ActionResult<LifecycleComparisonResponse> CompareLifecycles()
        {
            var result = _applicationService.GetLifecycleComparison();
            return Ok(result);
        }

    }
}
