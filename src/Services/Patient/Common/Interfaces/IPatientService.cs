using Patient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.Common.Interfaces
{
    public interface IPatientService
    {
        Task SavePatientAsync(PatientModel patient);

        Task<PatientModel> GetPatientByIdAsync(int id);

        Task<IEnumerable<PatientModel>> GetAllPatientsAsync();

        Task UpdatePatientAsync(PatientModel patient);

        Task DeletePatientAsync(int id);
    }
}
