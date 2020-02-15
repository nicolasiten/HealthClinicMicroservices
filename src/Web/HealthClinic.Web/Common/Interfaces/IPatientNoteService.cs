using HealthClinic.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthClinic.Web.Common.Interfaces
{
    public interface IPatientNoteService
    {
        Task CreateAsync(PatientNoteModel patientNoteModel);

        Task UpdateAsync(string id, PatientNoteModel patientNoteModel);

        Task<IEnumerable<PatientNoteModel>> GetAllAsync();

        Task<PatientNoteModel> GetByIdAsync(string id);

        Task<IEnumerable<PatientNoteModel>> GetByPatientIdAsync(int patientId);
    }
}
