using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthClinic.Web.Common.Interfaces;
using HealthClinic.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinic.Web.Controllers
{
    public class PatientNoteController : Controller
    {
        private readonly IPatientNoteService _patientNoteService;

        public PatientNoteController(IPatientNoteService patientNoteService)
        {
            _patientNoteService = patientNoteService;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var patientNote = await _patientNoteService.GetByIdAsync(id);

            return View(patientNote);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PatientNoteModel patientNoteModel)
        {
            await _patientNoteService.UpdateAsync(id, patientNoteModel);

            return RedirectToAction("Details", "Patient", new { id = patientNoteModel.PatientId });
        }

        [HttpGet]
        public async Task<IActionResult> Create(int patientId)
        {
            return View(new PatientNoteModel { PatientId = patientId });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientNoteModel patientNoteModel)
        {
            await _patientNoteService.CreateAsync(patientNoteModel);

            return RedirectToAction("Details", "Patient", new { id = patientNoteModel.PatientId });
        }
    }
}