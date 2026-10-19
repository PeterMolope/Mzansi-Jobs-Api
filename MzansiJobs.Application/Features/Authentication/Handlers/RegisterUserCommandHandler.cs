using MediatR;
using MzansiJobs.Application.Common.DTOs.Auth;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Authentication.Commands;
using MzansiJobs.Domain.Entities;
using MzansiJobs.Domain.Enums;
using MzansiJobs.Domain.Exceptions;

namespace MzansiJobs.Application.Features.Authentication.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;

        public RegisterUserCommandHandler(
            IUnitOfWork unitOfWork,
            IPasswordHasher passwordHasher,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // 1. Check if email is already registered
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new BadRequestException("A user with this email already exists.");
            }

            // 2. Hash the password
            string passwordHash = _passwordHasher.HashPassword(request.Password);

            // 3. Create the core User entity (using the UserRole enum)
            var user = new User(
                Guid.NewGuid(),
                request.Email,
                passwordHash,
                request.Role
            );

            await _unitOfWork.Users.AddAsync(user);

            // 4. If the registered user is a Candidate, create and link the Candidate profile
            string firstName = request.FirstName ?? string.Empty;
            string lastName = request.LastName ?? string.Empty;

            if (request.Role == UserRole.Candidate)
            {
                var candidate = new Candidate(

                    user.ID,
                    firstName,
                    lastName
                // Add any other initial candidate properties if your constructor requires them
                );

                await _unitOfWork.Candidates.AddAsync(candidate);
            }

            // 5. Generate JWT access token and refresh token
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshTokenString = _tokenService.GenerateRefreshToken();
            var expiresAt = DateTime.UtcNow.AddDays(7);

            // 6. Save the refresh token entity
            var refreshTokenEntity = new RefreshToken(
                refreshTokenString,
                user.ID,
                expiresAt

            );

            await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);

            // 7. Commit all changes via Unit of Work
            await _unitOfWork.CompleteAsync();

            // 8. Map and return response DTO
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
