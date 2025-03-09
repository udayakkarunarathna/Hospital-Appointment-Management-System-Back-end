namespace HospitalAppointmentManagementApp.Models
{
    using BCrypt.Net;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public class Patient
    {
        public int PatientID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
