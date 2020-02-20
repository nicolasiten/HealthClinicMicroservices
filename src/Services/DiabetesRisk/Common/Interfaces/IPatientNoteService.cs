using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesRisk.Common.Interfaces
{
    public interface IPatientNoteService
    {
        Task<IEnumerable<string>> GetNotesByPatientIdAsync(int patientId);
    }
}
