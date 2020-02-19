using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesRisk.Models
{
    public class PatientNoteModel
    {
        public string Id { get; set; }

        public int PatientId { get; set; }

        public string Note { get; set; }

        public DateTime Created { get; set; }

        public DateTime Edited { get; set; }
    }
}
