using BloodDonorSystem.Models;
using BloodDonorSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonorSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodRequestController : ControllerBase
    {
        private readonly IFindAndRequestBloodService _bloodService;

        public BloodRequestController(IFindAndRequestBloodService bloodService)
        {
            _bloodService = bloodService;
        }

        [HttpPost("request")]
        public IActionResult RequestBlood([FromBody] BloodRequestModel bloodRequest)
        {
            var result = _bloodService.RequestBlood(bloodRequest);
            return result;
        }

        [HttpGet("processQueue")]
        public IActionResult ProcessQueue()
        {
            var result = _bloodService.ProcessQueue();
            return result;
        }
    }
}
