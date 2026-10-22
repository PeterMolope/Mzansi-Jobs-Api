using MediatR;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Companies.Commands;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Features.Companies.Handlers
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(request.CompanyId);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID '{request.CompanyId}' was not found.");
            }

            company.UpdateDetails(
                request.Name,
                request.Description,
                request.Province,
                request.Industry,
                request.Website,
                request.LogoUrl
            );

            _unitOfWork.Repository<Company>().Update(company);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
