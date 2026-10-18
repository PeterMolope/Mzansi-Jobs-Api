using MediatR;
using MzansiJobs.Application.Common.DTOs.Auth;

namespace MzansiJobs.Application.Features.Authentication.Commands
{
    public class LoginUserCommand : IRequest<AuthResponseDTO>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
