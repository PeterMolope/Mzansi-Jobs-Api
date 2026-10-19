using MediatR;
using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Features.Jobs.Commands
{
    public record CreateJobCommand(
     string Title,
     string Description,
     Province Province,
     EmploymentType EmploymentType,
     Guid CompanyID,
     decimal? SalaryRange,
     DateTime? ExpirationDate
 ) : IRequest<Guid>;
}
