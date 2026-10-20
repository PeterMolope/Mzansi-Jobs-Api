using MediatR;
using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.DTOs.Candidates;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Candidates.Queries;

namespace MzansiJobs.Application.Features.Candidates.Handlers
{
    public class GetCandidateProfileQueryHandler : IRequestHandler<GetCandidateProfileQuery, CandidateProfileDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCandidateProfileQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CandidateProfileDTO> Handle(GetCandidateProfileQuery request, CancellationToken cancellationToken)
        {
            var profile = await _unitOfWork.Candidates
                .GetQueryable()
                .AsNoTracking()
                .Where(c => c.ID == request.CandidateId)
                .Select(c => new CandidateProfileDTO
                {
                    Id = c.ID,
                    UserId = c.UserID,
                    FirstName = c.firstName,
                    LastName = c.surname,
                    Email = c.User.Email, // Extracted directly through the User navigation property
                    Phone = c.phoneNumber,
                    CVUrl = c.CVUrl,
                    Bio = c.bio
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (profile == null)
            {
                throw new NotFoundException($"Candidate with ID '{request.CandidateId}' was not found.");
            }

            return profile;
        }
    }
}
