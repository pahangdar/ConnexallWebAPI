using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class AppointmentCreateDTO
    {
        [Required]
        public DateOnly Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        public string? Status { get; set; }
    }
}
