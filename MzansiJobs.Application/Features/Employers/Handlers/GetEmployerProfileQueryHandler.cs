using MediatR;
using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.DTOs.Employers;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Employers.Queries;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Features.Employers.Handlers
{
    public class GetEmployerProfileQueryHandler : IRequestHandler<GetEmployerProfileQuery, EmployerDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetEmployerProfileQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployerDTO> Handle(GetEmployerProfileQuery request, CancellationToken cancellationToken)
        {
            var employer = await _unitOfWork.Repository<Employer>()
                .GetQueryable()
                .AsNoTracking()
                .Where(e => e.ID == request.EmployerId)
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
                .FirstOrDefaultAsync(cancellationToken);

            if (employer == null)
            {
                throw new NotFoundException($"Employer profile with ID '{request.EmployerId}' was not found.");
            }

            return employer;
        }
    }
}
