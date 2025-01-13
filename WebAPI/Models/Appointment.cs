﻿namespace WebAPI.Models
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Status { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
    }

}
