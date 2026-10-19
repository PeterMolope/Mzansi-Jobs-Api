using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Common.Interfaces
{
    public interface ICandidateRepository : IGenericRepository<Candidate>
    {
        Task<Candidate?> GetByUserIdAsync(Guid userId);
    }
}
