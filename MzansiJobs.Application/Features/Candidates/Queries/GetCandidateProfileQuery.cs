using MediatR;
using MzansiJobs.Application.Common.DTOs.Candidates;

namespace MzansiJobs.Application.Features.Candidates.Queries
{
    public record GetCandidateProfileQuery(Guid CandidateId) : IRequest<CandidateProfileDTO>;
}
