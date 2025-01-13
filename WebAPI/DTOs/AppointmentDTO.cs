using System;

namespace WebAPI.DTOs
{
    public class AppointmentDTO
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
    }
}
