using WebAPI.Models;

namespace WebAPI.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public required DateTime Date { get; set; }
        public required TimeSpan Time { get; set; }
        public required int PatientID { get; set; }
        public required int DoctorID { get; set; }
        public string? Status { get; set; }

        // Navigation properties
        public required Patient Patient { get; set; }
        public required Doctor Doctor { get; set; }
    }

}
