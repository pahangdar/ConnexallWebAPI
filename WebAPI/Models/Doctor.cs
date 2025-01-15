namespace WebAPI.Models
{
    public class Doctor
    {
        public int DoctorID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Specialization { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
