using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentManagementApp.Models
{
    public class DoctorAvailability
    {
        public int AvailabilityID { get; set; }
        public int DoctorID { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public bool IsBooked { get; set; }
    }
}
