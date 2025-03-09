using System.Collections.Generic;

namespace HospitalAppointmentManagementApp.Models
{
    using Microsoft.EntityFrameworkCore;
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorAvailability> DoctorAvailabilities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>().HasKey(a => a.AppointmentID);      

            // Configure the Admin entity
            modelBuilder.Entity<Admin>().HasKey(a => a.AdminID);
            modelBuilder.Entity<Appointment>().HasKey(a => a.AppointmentID);
            modelBuilder.Entity<Doctor>().HasKey(a => a.DoctorID);
            modelBuilder.Entity<Patient>().HasKey(a => a.PatientID);

            // Configure the DoctorAvailability entity
            modelBuilder.Entity<DoctorAvailability>()
                .HasKey(da => da.AvailabilityID); // Ensure AvailabilityID is the primary key


        }    

    }
}
