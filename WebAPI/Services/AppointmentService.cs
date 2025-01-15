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

        public IEnumerable<Appointment> GetAppointmentsByDate(DateTime selectedDate)
        {
            return _context.Appointments
                .Where(a => a.Date.Date == selectedDate.Date)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToList();
        }

        public Appointment CreateAppointment(AppointmentDTO appointmentDto)
        {
            // Fetch related Patient and Doctor from the database
            var patient = _context.Patients.Find(appointmentDto.PatientID);
            var doctor = _context.Doctors.Find(appointmentDto.DoctorID);

            // Ensure Patient and Doctor exist
            if (patient == null)
                throw new ArgumentException($"Patient with ID {appointmentDto.PatientID} not found.");
            if (doctor == null)
                throw new ArgumentException($"Doctor with ID {appointmentDto.DoctorID} not found.");

            // Create the Appointment
            var appointment = new Appointment
            {
                Date = appointmentDto.Date,
                Time = appointmentDto.Time,
                Status = appointmentDto.Status,
                PatientID = appointmentDto.PatientID,
                DoctorID = appointmentDto.DoctorID,
                Patient = patient,
                Doctor = doctor
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
