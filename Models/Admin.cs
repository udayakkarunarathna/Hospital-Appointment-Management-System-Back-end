using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentManagementApp.Models
{
    public class Admin
    {
        public int AdminID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
