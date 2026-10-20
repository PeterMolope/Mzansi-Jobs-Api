using MediatR;
using MzansiJobs.Application.Common.DTOs.Jobs;

namespace MzansiJobs.Application.Features.Jobs.Queries
{
    public record GetJobByIdQuery(Guid Id) : IRequest<JobDetailsDTO>;
}
