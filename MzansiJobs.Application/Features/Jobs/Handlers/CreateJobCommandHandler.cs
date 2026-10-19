using MediatR;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Jobs.Commands;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Features.Jobs.Handlers
{
    internal class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateJobCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            // 1. Verify that the Company exists before posting a job
            var company = await _unitOfWork.Repository<Company>().GetByIdAsync(request.CompanyID);
            if (company == null)
            {
                throw new NotFoundException("Company profile not found.");
            }

            // 2. Create the Job entity using your entity's property structure
            // (If your Job entity uses a constructor, make sure to pass these parameters. 
            // Alternatively, if it uses private setters, you can adjust to use a constructor or factory method).
            var job = new Job(
                request.Title,
                request.Description,
                request.Province,
                request.EmploymentType,
                request.CompanyID,
                request.SalaryRange,
                request.ExpirationDate
            );

            // 3. Persist via Unit of Work
            await _unitOfWork.Jobs.AddAsync(job);
            await _unitOfWork.CompleteAsync();

            return job.ID;
        }
    }
}
