using MediatR;
using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.DTOs.Applying;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Applying.Queries;

namespace MzansiJobs.Application.Features.Applying.Handlers
{
    public class GetApplicationsByJobIdQueryHandler : IRequestHandler<GetApplicationsByJobIdQuery, IEnumerable<JobApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetApplicationsByJobIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<JobApplicationDTO>> Handle(GetApplicationsByJobIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Verify Job existence
            var job = await _unitOfWork.Jobs.GetByIdAsync(request.JobId);
            if (job == null)
            {
                throw new NotFoundException($"Job with ID '{request.JobId}' was not found.");
            }

            // 2. Retrieve applications linked to this job along with Candidate profile
            var applications = await _unitOfWork.Repository<MzansiJobs.Domain.Entities.Application>()
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.JobID == request.JobId)
                .Include(a => a.Candidate)
                .OrderByDescending(a => a.dateApplied)
                .Select(a => new JobApplicationDTO
                {
                    ApplicationId = a.ID,
                    CandidateId = a.CandidateID,
                    CandidateFirstName = a.Candidate != null ? a.Candidate.firstName : string.Empty,
                    CandidateLastName = a.Candidate != null ? a.Candidate.surname : string.Empty,
                    CoverLetter = a.coverLetter,
                    AppliedDate = a.dateApplied,
                    Status = a.Status
                })
                .ToListAsync(cancellationToken);

            return applications;
        }
    }
}
