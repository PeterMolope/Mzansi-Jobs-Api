using MediatR;
using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Features.Applying.Commands
{
    public record UpdateApplicationStatusCommand(
     Guid ApplicationId,
     ApplicationStatus NewStatus
 ) : IRequest<bool>;
}
