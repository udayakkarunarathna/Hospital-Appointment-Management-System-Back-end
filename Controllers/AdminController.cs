using HospitalAppointmentManagementApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // Restrict access to admins only
    public class AdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // Get all users (patients and doctors)
        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var patients = await _context.Patients.ToListAsync();
            var doctors = await _context.Doctors.ToListAsync();

            var users = new
            {
                Patients = patients,
                Doctors = doctors
            };

            return Ok(users);
        }

        // Get a specific user by ID
        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            var doctor = await _context.Doctors.FindAsync(id);

            if (patient == null && doctor == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            return Ok(patient ?? (object)doctor);
        }

        // Delete a user by ID
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            var doctor = await _context.Doctors.FindAsync(id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }
            else if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
            }
            else
            {
                return NotFound(new { Message = "User not found" });
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "User deleted successfully" });
        }

        // Get all appointments
        [HttpGet("appointments")]
        public async Task<IActionResult> GetAllAppointments()
        {
            var appointments = await _context.Appointments
                .ToListAsync();

            return Ok(appointments);
        }

        // Update an appointment status
        [HttpPut("appointments/{id}")]
        public async Task<IActionResult> UpdateAppointmentStatus(int id, [FromBody] string status)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound(new { Message = "Appointment not found" });
            }

            appointment.Status = status;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Appointment status updated successfully" });
        }

        // Delete an appointment by ID
        [HttpDelete("appointments/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound(new { Message = "Appointment not found" });
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Appointment deleted successfully" });
        }
    }
}
