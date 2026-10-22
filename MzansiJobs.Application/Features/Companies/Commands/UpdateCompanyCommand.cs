using MediatR;
using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Features.Companies.Commands
{
    public record UpdateCompanyCommand(
     Guid CompanyId,
     string Name,
     string? Description,
     Province Province,
     Industry Industry,
     string? Website,
     string? LogoUrl
 ) : IRequest<bool>;
}
