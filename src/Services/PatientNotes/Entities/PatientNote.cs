using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientNotes.Entities
{
    public class PatientNote : BaseEntity
    {
        public int PatientId { get; set; }

        public string Note { get; set; }
    }
}
