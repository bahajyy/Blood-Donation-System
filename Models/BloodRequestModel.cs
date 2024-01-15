using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonorSystem.Models
{
    public class BloodRequestModel
    {
        public required string BloodType { get; set; }
        public required string DonorName { get; set; }
        public int Units { get; set; }
        public required string City { get; set; }
        public required string Town { get; set; }
        public required string ContactEmail { get; set; }
    }
}