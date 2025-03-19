using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var doctors = _doctorService.GetAllDoctors();
            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var doctor = _doctorService.GetDoctorById(id);
            if (doctor == null)
                return NotFound();
            return Ok(doctor);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Doctor doctor)
        {
            var createdDoctor = _doctorService.CreateDoctor(doctor);
            return CreatedAtAction(nameof(GetById), new { id = createdDoctor.DoctorID }, createdDoctor);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Doctor doctor)
        {
            var updated = _doctorService.UpdateDoctor(id, doctor);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _doctorService.DeleteDoctor(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
