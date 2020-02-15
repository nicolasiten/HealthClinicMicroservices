using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HealthClinic.Web.Models
{
    public class PatientNoteModel
    {
        public string Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public string Note { get; set; }

        public DateTime Created { get; set; }

        public DateTime Edited { get; set; }
    }
}
