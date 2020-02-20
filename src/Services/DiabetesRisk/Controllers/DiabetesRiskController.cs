using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiabetesRisk.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiabetesRisk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiabetesRiskController : ControllerBase
    {
        private readonly IDiabetesRiskService _diabetesRiskService;

        public DiabetesRiskController(IDiabetesRiskService diabetesRiskService)
        {
            _diabetesRiskService = diabetesRiskService;
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult<string>> Get(int patientId)
        {
            var riskAssessment = await _diabetesRiskService.DetermineRiskForPatientAsync(patientId);

            return riskAssessment.Assessment;
        }
    }
}
