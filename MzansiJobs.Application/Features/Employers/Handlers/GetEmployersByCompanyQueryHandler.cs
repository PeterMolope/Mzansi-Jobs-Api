using MediatR;
using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.DTOs.Employers;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Employers.Queries;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Features.Employers.Handlers
{
    public class GetEmployersByCompanyQueryHandler : IRequestHandler<GetEmployersByCompanyQuery, IEnumerable<EmployerDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployersByCompanyQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployerDTO>> Handle(GetEmployersByCompanyQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Repository<Employer>()
                .GetQueryable()
                .AsNoTracking()
                .Where(e => e.CompanyID == request.CompanyId)
                .Select(e => new EmployerDTO
                {
                    Id = e.ID,
                    UserId = e.UserID,
                    CompanyId = e.CompanyID,
                    FirstName = e.firstName,
                    LastName = e.surname,
                    Email = e.User.Email,
                    JobTitle = e.jobTitle,
                    Phone = e.phoneNumber,
                    CompanyName = e.Company.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}
