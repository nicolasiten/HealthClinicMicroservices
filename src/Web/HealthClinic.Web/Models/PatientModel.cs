using HealthClinic.Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthClinic.Web.Models
{
    public class PatientModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Family { get; set; }

        [Required]
        [MaxLength(200)]
        public string Given { get; set; }

        [Required]
        [MaxDateTodayValidation]
        [DataType(DataType.Date)]
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

        public PatientNoteModel PatientNoteModel { get; set; }
    }
}
