using MediatR;
using MzansiJobs.Application.Common.DTOs.Applying;

namespace MzansiJobs.Application.Features.Applying.Queries
{
    public record GetCandidateApplicationsQuery(Guid CandidateId) : IRequest<IEnumerable<CandidateApplicationDTO>>;
}
