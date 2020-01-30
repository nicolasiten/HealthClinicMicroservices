using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patient.Common.Interfaces;
using Patient.Models;
using Patient.Services;

namespace Patient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpPost]
        public async Task<ActionResult> Create(PatientModel patientModel)
        {
            await _patientService.SavePatientAsync(patientModel);

            return Ok("success");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientModel>> Get(int id)
        {
            return await _patientService.GetPatientByIdAsync(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<List<PatientModel>>> GetAll()
        {
            return (await _patientService.GetAllPatientsAsync()).ToList();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, PatientModel patientModel)
        {
            if (id != patientModel.Id)
            {
                return BadRequest();
            }

            await _patientService.UpdatePatientAsync(patientModel);

            return Ok("success");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _patientService.DeletePatientAsync(id);

            return Ok("success");
        }
    }
}