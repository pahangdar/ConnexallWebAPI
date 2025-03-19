using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientsController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var patients = _patientService.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var patient = _patientService.GetPatientById(id);
            if (patient == null)
                return NotFound();
            return Ok(patient);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Patient patient)
        {
            var createdPatient = _patientService.CreatePatient(patient);
            return CreatedAtAction(nameof(GetById), new { id = createdPatient.PatientID }, createdPatient);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Patient patient)
        {
            var updated = _patientService.UpdatePatient(id, patient);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _patientService.DeletePatient(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet("search/{name}")]
        public IActionResult SearchByName(string name)
        {
            var patients = _patientService.SearchPatientsByName(name);
            return Ok(patients);
        }
    }
}
