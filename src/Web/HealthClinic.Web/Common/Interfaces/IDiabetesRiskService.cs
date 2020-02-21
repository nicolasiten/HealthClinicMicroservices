using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthClinic.Web.Common.Interfaces
{
    public interface IDiabetesRiskService
    {
        Task<string> GetByPatientId(int patientId);
    }
}
