using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using WebAPI.Models;
using WebAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentsController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var appointments = _appointmentService.GetAllAppointments();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var appointment = _appointmentService.GetAppointmentById(id);
            if (appointment == null)
                return NotFound();
            return Ok(appointment);
        }

        [HttpGet("by-date")]
        public IActionResult GetAppointmentsByDate([FromQuery] DateTime date)
        {
            try
            {
                var appointments = _appointmentService.GetAppointmentsByDate(date);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("date/{date}")]
        public IActionResult GetAppointmentsByDate2(DateTime date)
        {
            try
            {
                var appointments = _appointmentService.GetAppointmentsByDate(date);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] AppointmentCreateDTO appointmentCreateDto)
        {
            var createdAppointment = _appointmentService.CreateAppointment(appointmentCreateDto);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.AppointmentID }, createdAppointment);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AppointmentCreateDTO appointmentDto)
        {
            var updated = _appointmentService.UpdateAppointment(id, appointmentDto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _appointmentService.DeleteAppointment(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet("status/{status}")]
        public IActionResult GetByStatus(string status)
        {
            var appointments = _appointmentService.GetAppointmentsByStatus(status);
            return Ok(appointments);
        }

        [HttpPut("status/{id}")]
        public IActionResult UpdateStatus(int id, [FromQuery] string status)
        {
            var updated = _appointmentService.UpdateAppointmentStatus(id, status);
            if (!updated)
                return NotFound();
            return NoContent();
        }

    }
}
