using System;
using WebAPI.Models;
using WebAPI.Data;

namespace WebAPI.Services
{
    public class DoctorService
    {
        private readonly AppDbContext _context;

        public DoctorService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _context.Doctors.ToList();
        }

        public Doctor? GetDoctorById(int id)
        {
            return _context.Doctors.Find(id);
        }

        public Doctor CreateDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return doctor;
        }

        public bool UpdateDoctor(int id, Doctor doctor)
        {
            var existingDoctor = _context.Doctors.Find(id);
            if (existingDoctor == null) return false;

            existingDoctor.FirstName = doctor.FirstName;
            existingDoctor.LastName = doctor.LastName;
            existingDoctor.Specialization = doctor.Specialization;
            existingDoctor.PhoneNumber = doctor.PhoneNumber;
            existingDoctor.Address = doctor.Address;

            _context.SaveChanges();
            return true;
        }

        public bool DeleteDoctor(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return false;

            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return true;
        }
    }
}
