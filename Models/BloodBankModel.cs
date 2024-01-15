using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonorSystem.Models
{
    public class BloodBankModel
    {
        public string? Name { get; set; }
        public int Units { get; set; }  
        public List<string>? AvailableBloodTypes { get; set; } 
    }
}