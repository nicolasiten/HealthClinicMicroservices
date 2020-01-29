using Patient.Infrastructure;
using Patient.Interfaces;
using Patient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.Services
{
    public class PatientService : IPatientService
    {
        private readonly PatientDbContext _dbContext;

        public PatientService(PatientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SavePatientAsync(PatientModel patient)
        {
            await _dbContext.Patients.AddAsync(MapPatientModelToPatient(patient));

            await _dbContext.SaveChangesAsync();
        }

        private Entities.Patient MapPatientModelToPatient(PatientModel patientModel)
        {
            return new Entities.Patient
            {
                LastName = patientModel.Family,
                FirstName = patientModel.Given,
                Address = patientModel.Address,
                DateOfBirth = patientModel.Dob,
                Phone = patientModel.Phone,
                Sex = patientModel.Sex
            };
        }
    }
}
