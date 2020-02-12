using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PatientNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientNotesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> Create(int patientId, string notes)
        {

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