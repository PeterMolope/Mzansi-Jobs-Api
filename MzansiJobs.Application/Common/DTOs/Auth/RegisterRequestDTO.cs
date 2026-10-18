using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Common.DTOs.Auth
{
    public class RegisterRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string surname { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
