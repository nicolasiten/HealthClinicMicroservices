using HealthClinic.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthClinic.Web.Common.Interfaces
{
    public interface IPatientService
    {
        Task CreateAsync(PatientModel patientModel);

        Task<IEnumerable<PatientModel>> GetAllAsync();

        Task<PatientModel> GetByIdAsync(int id);

        Task UpdateAsync(int id, PatientModel patientModel);
    }
}
