using MediatR;
using MzansiJobs.Application.Common.DTOs.Auth;

namespace MzansiJobs.Application.Features.Authentication.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResponseDTO>
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}

