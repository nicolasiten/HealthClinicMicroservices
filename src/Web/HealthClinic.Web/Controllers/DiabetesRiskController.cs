using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthClinic.Web.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinic.Web.Controllers
{
    public class DiabetesRiskController : Controller
    {
        private readonly IDiabetesRiskService _diabetesRiskService;

        public DiabetesRiskController(IDiabetesRiskService diabetesRiskService)
        {
            _diabetesRiskService = diabetesRiskService;
        }

        [HttpGet]
        public async Task<IActionResult> Assessment(int patientId)
        {
            var assessment = await _diabetesRiskService.GetByPatientId(patientId);

            return View(nameof(Assessment), assessment);
        }
    }
}