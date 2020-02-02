using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthClinic.Web.Common.Interfaces;
using HealthClinic.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinic.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PatientModel patientModel)
        {
            _patientService.Create(patientModel);

            return RedirectToAction(nameof(Create));
        }
    }
}