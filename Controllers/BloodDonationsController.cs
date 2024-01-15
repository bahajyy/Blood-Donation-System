using BloodDonorSystem.Models;
using BloodDonorSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonorSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BloodDonationsController : ControllerBase
    {
        private readonly IBloodDonationService _bloodDonationService;

        public BloodDonationsController(IBloodDonationService bloodDonationService)
        {
            _bloodDonationService = bloodDonationService;
        }

        [HttpPost]
        public IActionResult AddBloodToBank([FromBody] BloodDonationModel bloodDonation)
        {
            return _bloodDonationService.AddBloodToBank(bloodDonation);
        }
    }
}
