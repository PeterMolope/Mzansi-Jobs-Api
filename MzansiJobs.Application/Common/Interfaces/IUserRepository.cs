
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Common.Interfaces

{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
}
