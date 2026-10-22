using MediatR;
using MzansiJobs.Application.Common.DTOs.Employers;

namespace MzansiJobs.Application.Features.Employers.Queries
{
    public record GetEmployersByCompanyQuery(Guid CompanyId) : IRequest<IEnumerable<EmployerDTO>>;
}
