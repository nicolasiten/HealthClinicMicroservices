using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientNotes.Models
{
    public class PatientNoteModel
    {
        public string Id { get; set; }

        public int PatientId { get; set; }

        public string Note { get; set; }
    }
}
