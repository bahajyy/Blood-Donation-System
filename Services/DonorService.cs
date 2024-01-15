using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonorSystem.Services
{
    public class DonorService : IDonorService
{
    private readonly List<DonorModel> _donors; 
    private readonly Dictionary<string, string> _branches;

    public DonorService()
    {
        _donors = new List<DonorModel>();
        _branches = new Dictionary<string, string>
        {
            {"Branch1", "123"},
            {"Branch2", "123"}
        };
    }

    public DonorModel CreateDonor(DonorModel donorModel, string branchName, string branchPassword)
    {
        if (AuthenticateBranch(branchName, branchPassword))
        {
            var existingDonor = _donors.FirstOrDefault(d => d.FullName == donorModel.FullName && d.BloodType == donorModel.BloodType);

            if (existingDonor == null)
            {
                var newDonor = new DonorModel
                {
                    Id = _donors.Count + 1,
                    FullName = donorModel.FullName,
                    BloodType = donorModel.BloodType,
                    Town = donorModel.Town,
                    City = donorModel.City,
                    PhoneNumber = donorModel.PhoneNumber,
                    Photo = donorModel.Photo
                };

                _donors.Add(newDonor);
                return newDonor;
            }
            else
            {
                return existingDonor;
            }
        }

        return null;
    }

    public List<DonorModel> GetAllDonors()
    {
        return _donors;
    }

    public DonorModel GetDonorById(int donorId)
    {
#pragma warning disable CS8603 // Possible null reference return.
            return _donors.FirstOrDefault(d => d.Id == donorId);
#pragma warning restore CS8603 // Possible null reference return.
        }

    private bool AuthenticateBranch(string branchName, string branchPassword)
    {
        return _branches.TryGetValue(branchName, out var storedPassword) && storedPassword == branchPassword;
    }
}

}