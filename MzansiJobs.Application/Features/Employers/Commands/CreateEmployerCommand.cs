using MediatR;

namespace MzansiJobs.Application.Features.Employers.Commands
{
    public record CreateEmployerCommand(
    Guid UserId,
    Guid CompanyId,
    string FirstName,
    string Surname,
    string JobTitle
) : IRequest<Guid>;
}
