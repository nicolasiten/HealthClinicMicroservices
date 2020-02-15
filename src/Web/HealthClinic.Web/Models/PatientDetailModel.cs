using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthClinic.Web.Models
{
    public class PatientDetailModel
    {
        public PatientModel Patient { get; set; }

        public IEnumerable<PatientNoteModel> PatientNotes { get; set; }
    }
}
