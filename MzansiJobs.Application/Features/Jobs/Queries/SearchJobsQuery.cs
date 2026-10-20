using MediatR;
using MzansiJobs.Application.Common.DTOs.Jobs;
using MzansiJobs.Application.Common.DTOs.Jobs.Pagination;
using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Features.Jobs.Queries
{
    public record SearchJobsQuery(
    string? SearchTerm = null,
    Province? Province = null,
    EmploymentType? EmploymentType = null,
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedListDTO<JobDetailsDTO>>;
}
