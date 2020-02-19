using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesRisk.Models
{
    public class PatientModel
    {
        public int Id { get; set; }

        public string Family { get; set; }

        public string Given { get; set; }

        public DateTime Dob { get; set; }

        public string Sex { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
