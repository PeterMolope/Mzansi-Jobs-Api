using MediatR;
using MzansiJobs.Application.Common.DTOs.Auth;
using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Features.Authentication.Commands
{
    internal class RegisterUserCommand : IRequest<AuthResponseDTO>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
