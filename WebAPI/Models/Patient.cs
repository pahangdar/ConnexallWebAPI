using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string FullName => $"{FirstName ?? ""} {LastName ?? ""}".Trim();
    }
}
