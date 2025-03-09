using HospitalAppointmentManagementApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HospitalAppointmentManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> ScheduleAppointment(Appointment appointment)
        {
            // Ensure the patient and doctor exist
            var patient = await _context.Patients.FindAsync(appointment.PatientID);
            var doctor = await _context.Doctors.FindAsync(appointment.DoctorID);

            if (patient == null || doctor == null)
            {
                return BadRequest(new { Message = "Invalid PatientID or DoctorID" });
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            // Send email notification (mock implementation)
            SendEmailNotification(appointment.PatientID, "Appointment Scheduled");

            return Ok(new { Message = "Appointment scheduled successfully!" });
        }

        private void SendEmailNotification(int patientId, string message)
        {
            // Mock email notification logic
            var patient = _context.Patients.Find(patientId);
            Console.WriteLine($"Email sent to {patient.Email}: {message}");
        }
    }
}
