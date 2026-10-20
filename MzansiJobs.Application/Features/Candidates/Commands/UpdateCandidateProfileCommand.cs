using MediatR;

namespace MzansiJobs.Application.Features.Candidates.Commands
{
    public record UpdateCandidateProfileCommand(
    Guid CandidateId,
    string FirstName,
    string LastName,
    string? Phone,
    string? ResumeUrl,
    string? Summary
) : IRequest<bool>;
}
