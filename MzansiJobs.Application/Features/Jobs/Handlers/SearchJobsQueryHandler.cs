using MediatR;
using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.DTOs.Jobs;
using MzansiJobs.Application.Common.DTOs.Jobs.Pagination;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Jobs.Queries;


namespace MzansiJobs.Application.Features.Jobs.Handlers
{
    public class SearchJobsQueryHandler : IRequestHandler<SearchJobsQuery, PaginatedListDTO<JobDetailsDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchJobsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedListDTO<JobDetailsDTO>> Handle(SearchJobsQuery request, CancellationToken cancellationToken)
        {
            // 1. Get queryable source for Jobs
            var query = _unitOfWork.Jobs.GetQueryable()
                .AsNoTracking()
                .Where(j => j.isActive); // Only fetch active job postings

            // 2. Filter by search term (Title or Description)
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();
                query = query.Where(j => j.Title.ToLower().Contains(term) ||
                                         j.Description.ToLower().Contains(term));
            }

            // 3. Filter by Province enum
            if (request.Province.HasValue)
            {
                query = query.Where(j => j.Province == request.Province.Value);
            }

            // 4. Filter by EmploymentType enum
            if (request.EmploymentType.HasValue)
            {
                query = query.Where(j => j.EmploymentType == request.EmploymentType.Value);
            }

            // 5. Calculate total count before pagination
            int totalCount = await query.CountAsync(cancellationToken);

            // 6. Order by newest jobs and execute paginated query
            var items = await query
                .OrderByDescending(j => j.postedDate)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(job => new JobDetailsDTO
                {
                    Id = job.ID,
                    Title = job.Title,
                    Description = job.Description,
                    Province = job.Province,
                    EmploymentType = job.EmploymentType,
                    CompanyId = job.CompanyID,
                    CompanyName = job.Company != null ? job.Company.Name : null,
                    SalaryRange = job.salaryRange,
                    PostedDate = job.postedDate,
                    ExpirationDate = job.expirationDate,
                    IsActive = job.isActive
                })
                .ToListAsync(cancellationToken);

            // 7. Return wrapped paginated result DTO
            return new PaginatedListDTO<JobDetailsDTO>(items, totalCount, request.PageNumber, request.PageSize);
        }
    }
}
