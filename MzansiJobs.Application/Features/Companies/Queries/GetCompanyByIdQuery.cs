using MediatR;
using MzansiJobs.Application.Common.DTOs.Companies;

namespace MzansiJobs.Application.Features.Companies.Queries
{
    public record GetCompanyByIdQuery(Guid CompanyId) : IRequest<CompanyDTO>;
}
