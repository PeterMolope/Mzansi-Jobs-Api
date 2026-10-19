using MediatR;
using MzansiJobs.Application.Common.DTOs.Auth;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Authentication.Commands;
using MzansiJobs.Domain.Entities;
using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Features.Authentication.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            // 1. Fetch core authentication user by email
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException("Invalid email or password.");
            }

            // 2. Verify password
            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.passwordHash);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // 3. Resolve profile details (FirstName / LastName) based on User Role
            string firstName = string.Empty;
            string lastName = string.Empty;

            if (user.UserRole == UserRole.Candidate)
            {
                var candidate = await _unitOfWork.Candidates.GetByUserIdAsync(user.ID);
                if (candidate != null)
                {
                    firstName = candidate.firstName;
                    lastName = candidate.surname;
                }
            }
            // Add Employer/Admin profile lookup here when those repositories are wired up

            // 4. Generate JWT access token and refresh token
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshTokenString = _tokenService.GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddDays(7);

            // 5. Save refresh token entity to database
            var refreshTokenEntity = new RefreshToken(
                refreshTokenString,
                user.ID,
                expiresAt

            );

            await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);
            await _unitOfWork.CompleteAsync();

            // 6. Map and return full AuthResponseDto
            return new AuthResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenString,
                RefreshTokenExpiration = expiresAt,
                UserId = user.ID,
                Email = user.Email,
                FirstName = firstName,
                LastName = lastName,
                Role = user.UserRole
            };
        }
    }
}
