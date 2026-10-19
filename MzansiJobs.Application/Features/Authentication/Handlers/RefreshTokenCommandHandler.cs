using MediatR;
using MzansiJobs.Application.Common.DTOs.Auth;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Authentication.Commands;
using MzansiJobs.Domain.Entities;
using MzansiJobs.Domain.Enums;
using MzansiJobs.Domain.Exceptions;

namespace MzansiJobs.Application.Features.Authentication.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(
            IUnitOfWork unitOfWork,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // 1. Fetch token from persistence
            var storedRefreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(request.RefreshToken);

            if (storedRefreshToken == null)
            {
                throw new UnauthorizedException("Invalid refresh token.");
            }

            // 2. Validate expiration
            if (storedRefreshToken.expiresOn <= DateTime.UtcNow)
            {
                _unitOfWork.RefreshTokens.Remove(storedRefreshToken);
                await _unitOfWork.CompleteAsync();
                throw new UnauthorizedException("Refresh token has expired. Please log in again.");
            }

            // 3. Fetch linked user identity
            var user = await _unitOfWork.Users.GetByIdAsync(storedRefreshToken.UserID);

            if (user == null)
            {
                throw new UnauthorizedException("User associated with this token no longer exists.");
            }

            // 4. Retrieve profile metadata if candidate
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

            // 5. Rotate refresh token (revoke old, generate new)
            _unitOfWork.RefreshTokens.Remove(storedRefreshToken);

            var newAccessToken = _tokenService.GenerateAccessToken(user);
            var newRefreshTokenString = _tokenService.GenerateRefreshToken();
            var newExpiresAt = DateTime.UtcNow.AddDays(7);

            var newRefreshTokenEntity = new RefreshToken(
                newRefreshTokenString,
                user.ID,
                newExpiresAt

            );

            await _unitOfWork.RefreshTokens.AddAsync(newRefreshTokenEntity);

            // 6. Commit transaction
            await _unitOfWork.CompleteAsync();

            // 7. Return refreshed Auth DTO
            return new AuthResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshTokenString,
                RefreshTokenExpiration = newExpiresAt,
                UserId = user.ID,
                Email = user.Email,
                FirstName = firstName,
                LastName = lastName,
                Role = user.UserRole
            };
        }
    }
}
