using WebAPI.Models;
using WebAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using WebAPI.Data;

namespace WebAPI.Services
{
    public class AppointmentService
    {
        private readonly AppDbContext _context;

        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Appointment> GetAllAppointments()
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToList();
        }

        public Appointment? GetAppointmentById(int id)
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.AppointmentID == id);
        }

        public Appointment CreateAppointment(AppointmentDTO appointmentDto)
        {
            var appointment = new Appointment
            {
                Date = appointmentDto.Date,
                Time = appointmentDto.Time,
                Status = appointmentDto.Status,
                PatientID = appointmentDto.PatientID,
                DoctorID = appointmentDto.DoctorID
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return appointment;
        }

        public bool UpdateAppointment(int id, AppointmentDTO appointmentDto)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return false;

            appointment.Date = appointmentDto.Date;
            appointment.Time = appointmentDto.Time;
            appointment.Status = appointmentDto.Status;
            appointment.PatientID = appointmentDto.PatientID;
            appointment.DoctorID = appointmentDto.DoctorID;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteAppointment(int id)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return false;

            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Appointment> GetAppointmentsByStatus(string status)
        {
            return _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
