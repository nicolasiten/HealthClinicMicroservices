using Patient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.Interfaces
{
    public interface IPatientService
    {
        Task SavePatientAsync(PatientModel patient);
    }
}
