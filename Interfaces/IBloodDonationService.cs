// Services/IBloodDonationService.cs
using Microsoft.AspNetCore.Mvc;

namespace BloodDonorSystem.Services
{
    public interface IBloodDonationService
    {
        IActionResult AddBloodToBank(BloodDonationModel bloodDonation);
    }
}
