using MediatR;
using MzansiJobs.Application.Common.DTOs.Jobs;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Jobs.Queries;

namespace MzansiJobs.Application.Features.Jobs.Handlers
{
    public class GetJobByIdQueryHandler : IRequestHandler<GetJobByIdQuery, JobDetailsDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetJobByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<JobDetailsDTO> Handle(GetJobByIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Fetch job from persistence
            var job = await _unitOfWork.Jobs.GetByIdAsync(request.Id);

            if (job == null)
            {
                throw new NotFoundException($"Job with ID '{request.Id}' was not found.");
            }

            // 2. Map domain entity to DTO
            return new JobDetailsDTO
            {
                Id = job.ID,
                Title = job.Title,
                Description = job.Description,
                Province = job.Province,
                EmploymentType = job.EmploymentType,
                CompanyId = job.CompanyID,
                CompanyName = job.Company?.Name, // Populated if navigation property is included
                SalaryRange = job.salaryRange,
                PostedDate = job.postedDate,
                ExpirationDate = job.expirationDate,
                IsActive = job.isActive
            };
        }

    }
}
