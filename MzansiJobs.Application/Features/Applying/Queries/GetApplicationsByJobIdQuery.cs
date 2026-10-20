using MediatR;
using MzansiJobs.Application.Common.DTOs.Applying;

namespace MzansiJobs.Application.Features.Applying.Queries
{
    public record GetApplicationsByJobIdQuery(Guid JobId) : IRequest<IEnumerable<JobApplicationDTO>>;
}
