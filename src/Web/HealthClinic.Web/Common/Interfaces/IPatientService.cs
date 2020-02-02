using HealthClinic.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthClinic.Web.Common.Interfaces
{
    public interface IPatientService
    {
        Task Create(PatientModel patientModel);
    }
}
