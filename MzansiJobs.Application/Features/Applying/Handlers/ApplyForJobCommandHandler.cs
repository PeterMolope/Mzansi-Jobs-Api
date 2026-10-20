using MediatR;
using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Applying.Commands;


namespace MzansiJobs.Application.Features.Applying.Handlers
{
    public class ApplyForJobCommandHandler : IRequestHandler<ApplyForJobCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplyForJobCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(ApplyForJobCommand request, CancellationToken cancellationToken)
        {
            // 1. Verify Job exists and is active
            var job = await _unitOfWork.Jobs.GetByIdAsync(request.JobId);
            if (job == null || !job.isActive)
            {
                throw new NotFoundException("The specified job is not available or has been closed.");
            }

            // 2. Verify Candidate exists
            var candidate = await _unitOfWork.Candidates.GetByIdAsync(request.CandidateId);
            if (candidate == null)
            {
                throw new NotFoundException("Candidate profile not found.");
            }

            // 3. Check for existing application
            var existingApplication = await _unitOfWork.Repository<MzansiJobs.Domain.Entities.Application>()
                .GetQueryable()
                .FirstOrDefaultAsync(a => a.JobID == request.JobId && a.CandidateID == request.CandidateId, cancellationToken);

            if (existingApplication != null)
            {
                throw new MzansiJobs.Domain.Exceptions.BadRequestException("You have already applied for this position.");
            }

            // 4. Instantiate Application entity
            var application = new MzansiJobs.Domain.Entities.Application(

                request.JobId,
                request.CandidateId,
                request.CoverLetter
            );

            // 5. Persist via Unit of Work
            await _unitOfWork.Repository<MzansiJobs.Domain.Entities.Application>().AddAsync(application);
            await _unitOfWork.CompleteAsync();

            return application.ID;
        }
    }
}
