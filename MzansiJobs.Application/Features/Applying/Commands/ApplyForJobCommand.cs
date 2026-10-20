using MediatR;

namespace MzansiJobs.Application.Features.Applying.Commands
{
    public record ApplyForJobCommand(
    Guid JobId,
    Guid CandidateId,
    string? CoverLetter = null
) : IRequest<Guid>;
}
