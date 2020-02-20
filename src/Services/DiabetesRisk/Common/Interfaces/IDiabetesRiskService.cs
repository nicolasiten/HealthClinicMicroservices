using DiabetesRisk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesRisk.Common.Interfaces
{
    public interface IDiabetesRiskService
    {
        Task<RiskAssessmentModel> DetermineRiskForPatientAsync(int patientId);
    }
}
