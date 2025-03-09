using HospitalAppointmentManagementApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // Admin registration
        [HttpPost("admin/register")]
        public async Task<IActionResult> RegisterAdmin(Admin admin)
        {
            // Hash the password before saving
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(admin.PasswordHash);
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Admin registered successfully" });
        }

        // Admin login
        [HttpPost("admin/login")]
        public async Task<IActionResult> LoginAdmin(LoginRequest request)
        {
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Email == request.Email);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.PasswordHash))
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            var token = _jwtService.GenerateToken(admin.Email, "Admin");
            return Ok(new { Token = token });
        }

        // Patient registration (requires admin token)
        [HttpPost("patient/register")]
        [Authorize(Roles = "Admin")] // Only admins can register patients
        public async Task<IActionResult> RegisterPatient(Patient patient)
        {
            // Hash the password before saving
            patient.PasswordHash = BCrypt.Net.BCrypt.HashPassword(patient.PasswordHash);
            patient.CreatedAt = DateTime.UtcNow;

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Patient registered successfully" });
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            // Implement password verification logic (e.g., using BCrypt)
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }

}
