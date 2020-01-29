using Patient.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.Models
{
    public class PatientModel
    {
        [Required]
        [MaxLength(200)]
        public string Family { get; set; }

        [Required]
        [MaxLength(200)]
        public string Given { get; set; }

        [Required]
        [MaxDateTodayValidation]
        public DateTime Dob { get; set; }

        [Required]
        [MaxLength(20)]
        public string Sex { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
    }
}
