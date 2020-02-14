using Microsoft.EntityFrameworkCore;
using Patient.Infrastructure;
using Patient.Common.Interfaces;
using Patient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthClinic.Common.Exceptions;

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

        public async Task<PatientModel> GetPatientByIdAsync(int id)
        {
            var patientEntity = await _dbContext.Patients.FindAsync(id);

            if (patientEntity == null)
            {
                throw new NotFoundException("Patient", id);
            }

            return MapPatientEntityToPatientModel(patientEntity);
        }

        public async Task<IEnumerable<PatientModel>> GetAllPatientsAsync()
        {
            List<PatientModel> patientModels = new List<PatientModel>();

            foreach (var patient in await _dbContext.Patients.ToListAsync())
            {
                patientModels.Add(MapPatientEntityToPatientModel(patient));
            }

            return patientModels;
        }

        public async Task UpdatePatientAsync(PatientModel patient)
        {
            var patientEntity = await _dbContext.Patients.FindAsync(patient.Id);

            if (patientEntity == null)
            {
                throw new NotFoundException("Patient", patient.Id);
            }

            MapPatientModelToPatient(patient, patientEntity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patientEntity = await _dbContext.Patients.FindAsync(id);

            if (patientEntity == null)
            {
                throw new NotFoundException("Patient", id);
            }

            _dbContext.Patients.Remove(patientEntity);

            await _dbContext.SaveChangesAsync();
        }

        private PatientModel MapPatientEntityToPatientModel(Entities.Patient patientEntity)
        {
            return new PatientModel
            {
                Id = patientEntity.Id,
                Family = patientEntity.LastName,
                Given = patientEntity.FirstName,
                Dob = patientEntity.DateOfBirth,
                Address = patientEntity.Address,
                Phone = patientEntity.Phone,
                Sex = patientEntity.Sex
            };
        }

        private Entities.Patient MapPatientModelToPatient(PatientModel patientModel, Entities.Patient patient = null)
        {
            if (patient == null)
            {
                patient = new Entities.Patient();
            }

            patient.Id = patientModel.Id;
            patient.LastName = patientModel.Family;
            patient.FirstName = patientModel.Given;
            patient.Address = patientModel.Address;
            patient.DateOfBirth = patientModel.Dob;
            patient.Phone = patientModel.Phone;
            patient.Sex = patientModel.Sex;

            return patient;
        }
    }
}
