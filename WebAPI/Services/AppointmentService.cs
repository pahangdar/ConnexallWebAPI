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
            var selectedDateOnly = DateOnly.FromDateTime(selectedDate);

            return _context.Appointments
                .Where(a => a.Date == selectedDateOnly)
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                //.Select(a => new AppointmentDTO
                //{
                //    AppointmentID = a.AppointmentID,
                //    Date = a.Date,
                //    Time = a.Time,
                //    Status = a.Status,
                //    PatientID = a.PatientID,
                //    DoctorID = a.DoctorID,
                //    PatientFullName = a.Patient.FullName,
                //    DoctorFullName = a.Doctor.FullName
                //})
                .ToList();
        }

        public Appointment CreateAppointment(AppointmentCreateDTO appointmentCreateDto)
        {
            // Fetch related Patient and Doctor from the database
            var patient = _context.Patients.Find(appointmentCreateDto.PatientID);
            var doctor = _context.Doctors.Find(appointmentCreateDto.DoctorID);

            // Ensure Patient and Doctor exist
            if (patient == null)
                throw new ArgumentException($"Patient with ID {appointmentCreateDto.PatientID} not found.");
            if (doctor == null)
                throw new ArgumentException($"Doctor with ID {appointmentCreateDto.DoctorID} not found.");

            // Create the Appointment
            var appointment = new Appointment
            {
                Date = appointmentCreateDto.Date,
                Time = appointmentCreateDto.Time,
                Status = appointmentCreateDto.Status,
                PatientID = appointmentCreateDto.PatientID,
                DoctorID = appointmentCreateDto.DoctorID,
                Patient = patient,
                Doctor = doctor
            };

            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            return appointment;
        }

        public bool UpdateAppointment(int id, AppointmentCreateDTO appointmentCreateDto)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return false;

            appointment.Date = appointmentCreateDto.Date;
            appointment.Time = appointmentCreateDto.Time;
            appointment.Status = appointmentCreateDto.Status;
            appointment.PatientID = appointmentCreateDto.PatientID;
            appointment.DoctorID = appointmentCreateDto.DoctorID;

            _context.SaveChanges();
            return true;
        }

        public bool UpdateAppointmentStatus(int id, string status)
        {
            var appointment = _context.Appointments.Find(id);
            if (appointment == null) return false;

            appointment.Status = status;

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
