using MzansiJobs.Domain.Entities;
using MzansiJobs.Domain.Enums;

namespace MzansiJobs.Application.Common.Interfaces
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<IEnumerable<Job>> SearchJobsAsync(string? keyword, Province? province);
        Task<Job?> GetJobWithDetailsAsync(Guid id);
    }
}
