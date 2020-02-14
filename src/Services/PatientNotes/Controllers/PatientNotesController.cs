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

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<PatientNoteModel>>> GetAll()
        {
            return (await _patientNoteService.GetPatientNotesAsync()).ToList();
        }

        [HttpGet("{patientId}")]
        public async Task<ActionResult<List<PatientNoteModel>>> GetAllByPatientId(int patientId)
        {
            return (await _patientNoteService.GetPatientNotesByPatientIdAsync(patientId)).ToList();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, PatientNoteModel patientNoteModel)
        {
            if (id != patientNoteModel.Id)
            {
                return BadRequest();
            }

            await _patientNoteService.UpdatePatientAsync(patientNoteModel);

            return Ok("success");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _patientNoteService.DeletePatientAsync(id);

            return Ok("success");
        }        
    }
}