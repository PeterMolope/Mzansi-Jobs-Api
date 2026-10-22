using MediatR;
using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.DTOs.Companies;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Companies.Queries;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Features.Companies.Handlers
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, CompanyDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CompanyDTO> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Repository<Company>()
                .GetQueryable()
                .AsNoTracking()
                .Where(c => c.ID == request.CompanyId)
                .Select(c => new CompanyDTO
                {
                    Id = c.ID,
                    Name = c.Name,
                    Description = c.Description,
                    Province = c.Province,
                    Industry = c.Industry,
                    Website = c.Website,
                    LogoUrl = c.logoUrl
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID '{request.CompanyId}' was not found.");
            }

            return company;
        }
    }
}
