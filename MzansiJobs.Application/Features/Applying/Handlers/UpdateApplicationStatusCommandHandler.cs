using MediatR;
using MzansiJobs.Application.Common.Exceptions;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Application.Features.Applying.Commands;


namespace MzansiJobs.Application.Features.Applying.Handlers
{
    public class UpdateApplicationStatusCommandHandler : IRequestHandler<UpdateApplicationStatusCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateApplicationStatusCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateApplicationStatusCommand request, CancellationToken cancellationToken)
        {
            // 1. Fetch application from persistence
            var application = await _unitOfWork.Repository<MzansiJobs.Domain.Entities.Application>().GetByIdAsync(request.ApplicationId);

            if (application == null)
            {
                throw new NotFoundException($"Application with ID '{request.ApplicationId}' was not found.");
            }

            // 2. Update status (via domain method or direct property setter)
            application.UpdateStatus(request.NewStatus);

            // 3. Persist changes
            _unitOfWork.Repository<MzansiJobs.Domain.Entities.Application>().Update(application);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
