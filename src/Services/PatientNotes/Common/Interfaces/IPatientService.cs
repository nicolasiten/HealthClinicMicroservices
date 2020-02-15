using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientNotes.Common.Interfaces
{
    public interface IPatientService
    {
        Task<bool> PatientExists(int patientId);
    }
}
