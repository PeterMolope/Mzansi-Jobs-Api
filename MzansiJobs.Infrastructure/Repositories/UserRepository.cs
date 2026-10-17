using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Domain.Entities;
using MzansiJobs.Infrastructure.Persistence;

namespace MzansiJobs.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email);
        }
    }
}
