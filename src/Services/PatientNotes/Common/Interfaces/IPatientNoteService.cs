using PatientNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientNotes.Common.Interfaces
{
    public interface IPatientNoteService
    {
        Task SavePatientNoteAsync(PatientNoteModel patientNote);

        Task<IEnumerable<PatientNoteModel>> GetPatientNotesAsync();

        Task<IEnumerable<PatientNoteModel>> GetPatientNotesByPatientIdAsync(int patientId);

        Task<PatientNoteModel> GetPatientNoteByIdAsync(string id);

        Task UpdatePatientAsync(PatientNoteModel patientNoteModel);

        Task DeletePatientAsync(string patientNoteId);
    }
}
