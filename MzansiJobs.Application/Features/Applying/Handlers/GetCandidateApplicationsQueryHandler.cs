using MediatR;
using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.DTOs.Applying;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Applying.Queries;

namespace MzansiJobs.Application.Features.Applying.Handlers
{
    public class GetCandidateApplicationsQueryHandler : IRequestHandler<GetCandidateApplicationsQuery, IEnumerable<CandidateApplicationDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCandidateApplicationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CandidateApplicationDTO>> Handle(GetCandidateApplicationsQuery request, CancellationToken cancellationToken)
        {
            // 1. Verify candidate existence
            var candidate = await _unitOfWork.Candidates.GetByIdAsync(request.CandidateId);
            if (candidate == null)
            {
                throw new NotFoundException("Candidate profile not found.");
            }

            // 2. Query applications for the specified candidate using GetQueryable()
            var applications = await _unitOfWork.Repository<MzansiJobs.Domain.Entities.Application>()
                .GetQueryable()
                .AsNoTracking()
                .Where(a => a.CandidateID == request.CandidateId)
                .Include(a => a.Job)
                    .ThenInclude(j => j.Company)
                .OrderByDescending(a => a.dateApplied)
                .Select(a => new CandidateApplicationDTO
                {
                    ApplicationId = a.ID,
                    JobId = a.JobID,
                    JobTitle = a.Job.Title,
                    CompanyName = a.Job.Company != null ? a.Job.Company.Name : null,
                    Province = a.Job.Province,
                    EmploymentType = a.Job.EmploymentType,
                    CoverLetter = a.coverLetter,
                    AppliedDate = a.dateApplied,
                    Status = a.Status // Assigned directly as the enum type
                })
                .ToListAsync(cancellationToken);

            return applications;
        }
    }
}
