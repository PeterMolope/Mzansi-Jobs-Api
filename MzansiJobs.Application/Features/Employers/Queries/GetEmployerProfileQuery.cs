using MediatR;
using MzansiJobs.Application.Common.DTOs.Employers;

namespace MzansiJobs.Application.Features.Employers.Queries
{
    public record GetEmployerProfileQuery(Guid EmployerId) : IRequest<EmployerDTO>;
}
