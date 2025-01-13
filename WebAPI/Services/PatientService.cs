using System;
using WebAPI.Models;
using WebAPI.Data;

namespace WebAPI.Services
{
    public class PatientService
    {
        private readonly AppDbContext _context;

        public PatientService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient? GetPatientById(int id)
        {
            return _context.Patients.Find(id);
        }

        public Patient CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient;
        }

        public bool UpdatePatient(int id, Patient patient)
        {
            var existingPatient = _context.Patients.Find(id);
            if (existingPatient == null) return false;

            existingPatient.FirstName = patient.FirstName;
            existingPatient.LastName = patient.LastName;
            existingPatient.PhoneNumber = patient.PhoneNumber;
            existingPatient.Address = patient.Address;
            existingPatient.DateOfBirth = patient.DateOfBirth;
            existingPatient.Email = patient.Email;

            _context.SaveChanges();
            return true;
        }

        public bool DeletePatient(int id)
        {
            var patient = _context.Patients.Find(id);
            if (patient == null) return false;

            _context.Patients.Remove(patient);
            _context.SaveChanges();
            return true;
        }

        public IEnumerable<Patient> SearchPatientsByName(string name)
        {
            return _context.Patients
                .Where(p => p.FirstName.Contains(name) || p.LastName.Contains(name))
                .ToList();
        }
    }
}
