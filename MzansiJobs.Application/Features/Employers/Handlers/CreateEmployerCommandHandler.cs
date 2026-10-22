using MediatR;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Employers.Commands;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Features.Employers.Handlers
{
    public class CreateEmployerCommandHandler : IRequestHandler<CreateEmployerCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateEmployerCommand request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                throw new NotFoundException($"Company with ID '{request.CompanyId}' was not found.");
            }

            // Matches your exact constructor: Employer(Guid UserID, Guid CompanyID, string firstName, string surname, string jobTitle)
            var employer = new Employer(
                request.UserId,
                request.CompanyId,
                request.FirstName,
                request.Surname,
                request.JobTitle
            );

            await _unitOfWork.Repository<Employer>().AddAsync(employer);
            await _unitOfWork.CompleteAsync();

            return employer.ID;
        }
    }
}
