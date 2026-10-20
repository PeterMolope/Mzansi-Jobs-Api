using MediatR;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Candidates.Commands;

namespace MzansiJobs.Application.Features.Candidates.Handlers
{
    public class UpdateCandidateProfileCommandHandler : IRequestHandler<UpdateCandidateProfileCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCandidateProfileCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCandidateProfileCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.Candidates.GetByIdAsync(request.CandidateId);

            if (candidate == null)
            {
                throw new NotFoundException($"Candidate profile with ID '{request.CandidateId}' was not found.");
            }

            // Directly calls your candidate entity domain method:
            candidate.UpdateProfile(
                request.FirstName,
                request.LastName,
                request.Phone,
                request.ResumeUrl,
                request.Summary
            );

            _unitOfWork.Candidates.Update(candidate);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
