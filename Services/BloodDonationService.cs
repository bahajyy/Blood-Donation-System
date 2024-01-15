using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BloodDonorSystem.Services
{
    public class BloodDonationService : IBloodDonationService
    {
        private static int BloodBankQuantity = 0;

        public IActionResult AddBloodToBank(BloodDonationModel bloodDonation)
        {
            if (!AuthenticateBranch(bloodDonation.BranchName, bloodDonation.BranchPassword))
            {
                return new UnauthorizedObjectResult("Authentication failed for the branch.");
            }

            if (string.IsNullOrWhiteSpace(bloodDonation.BloodType) || string.IsNullOrWhiteSpace(bloodDonation.DonorName) || bloodDonation.Units <= 0)
            {
                return new BadRequestObjectResult("Invalid input data.");
            }

            BloodBankQuantity += bloodDonation.Units;

            var responseMessage = $"Blood added to the bank successfully. Blood Type: {bloodDonation.BloodType}, Donor Name: {bloodDonation.DonorName}, Units: {bloodDonation.Units}, Donation Date: {bloodDonation.DonationDate}, Branch Name: {bloodDonation.BranchName}";

            return new OkObjectResult(responseMessage);
        }

        private bool AuthenticateBranch(string branchName, string branchPassword)
        {
            var validBranches = new Dictionary<string, string>
            {
                {"Branch1", "Password1"},
                {"Branch2", "Password2"}
            };

            return validBranches.TryGetValue(branchName, out var storedPassword) && storedPassword == branchPassword;
        }
    }
}
