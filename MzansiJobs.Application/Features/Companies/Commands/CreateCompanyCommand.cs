using MediatR;
using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Features.Companies.Commands
{
    public record CreateCompanyCommand(
    string Name,
    string? Description,
    Province Province,
    Industry Industry
) : IRequest<Guid>;
}
