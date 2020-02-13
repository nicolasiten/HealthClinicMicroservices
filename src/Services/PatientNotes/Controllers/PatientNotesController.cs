using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientNotes.Common.Interfaces;
using PatientNotes.Models;

namespace PatientNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientNotesController : ControllerBase
    {
        private readonly IPatientNoteService _patientNoteService;

        public PatientNotesController(IPatientNoteService patientNoteService)
        {
            _patientNoteService = patientNoteService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(PatientNoteModel patientNoteModel)
        {
            await _patientNoteService.SavePatientNoteAsync(patientNoteModel);

            return Ok("success");
        }

        // TODO GET

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string notes)
        {

            return Ok("success");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete()
        {

            return Ok("success");
        }        
    }
}