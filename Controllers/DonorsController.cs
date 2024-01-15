using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonorSystem.Controllers
{

    [ApiController]
[Route("api/[controller]")]
public class DonorsController : ControllerBase
{
    private readonly IDonorService _donorService;

    public DonorsController(IDonorService donorService)
    {
        _donorService = donorService;
    }

    [HttpPost]
    [Route("create")]
    public IActionResult CreateDonor([FromBody] DonorModel donorModel, [FromHeader] string branchName, [FromHeader] string branchPassword)
    {
        var createdDonor = _donorService.CreateDonor(donorModel, branchName, branchPassword);

        if (createdDonor != null)
        {
            return Ok(createdDonor);
        }

        return Unauthorized("Authentication failed for the branch.");
    }

    [HttpGet]
    [Route("all")]
    public IActionResult GetAllDonors()
    {
        var allDonors = _donorService.GetAllDonors();
        return Ok(allDonors);
    }

    [HttpGet]
    [Route("{donorId}")]
    public IActionResult GetDonorById(int donorId)
    {
        var donor = _donorService.GetDonorById(donorId);

        if (donor != null)
        {
            return Ok(donor);
        }

        return NotFound("Donor not found.");
    }
}

}