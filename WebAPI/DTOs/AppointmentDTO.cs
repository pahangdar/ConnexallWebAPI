using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }
        public required DateTime Date { get; set; }
        public required TimeSpan Time { get; set; }
        public required int PatientID { get; set; }
        public required int DoctorID { get; set; }
        public required string? Status { get; set; }
        public string? PatientFullName { get; set; }
        public string? DoctorFullName { get; set; }
    }
}
