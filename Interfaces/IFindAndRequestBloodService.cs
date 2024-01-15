using BloodDonorSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonorSystem.Services
{
    public interface IFindAndRequestBloodService
    {
        IActionResult RequestBlood(BloodRequestModel bloodRequest);

        IActionResult ProcessQueue();
    }
}
