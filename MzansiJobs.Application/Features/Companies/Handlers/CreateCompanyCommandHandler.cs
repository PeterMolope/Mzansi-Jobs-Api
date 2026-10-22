using MediatR;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Companies.Commands;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Features.Companies.Handlers
{
    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = new Company(
                request.Name,
                request.Description ?? string.Empty,
                request.Province,
                request.Industry
            );

            await _unitOfWork.Repository<Company>().AddAsync(company);
            await _unitOfWork.CompleteAsync();

            return company.ID;
        }
    }
}
