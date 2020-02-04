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
        public async Task<ActionResult> Create(PatientModel patientModel)
        {
            await _patientService.CreateAsync(patientModel);

            return RedirectToAction(nameof(ViewAll));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);

            return View(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PatientModel patientModel)
        {
            await _patientService.UpdateAsync(id, patientModel);

            return RedirectToAction(nameof(ViewAll));
        }

        [HttpGet]
        public async Task<ActionResult> ViewAll()
        {
            return View(await _patientService.GetAllAsync());
        }

        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            return View(await _patientService.GetByIdAsync(id));
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            await _patientService.DeleteAsync(id);

            return RedirectToAction(nameof(ViewAll));
        }
    }
}