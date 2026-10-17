using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Domain.Entities;
using MzansiJobs.Infrastructure.Persistence;

namespace MzansiJobs.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _dbSet
                .Include(rt => rt.User) // Optional: if you want to immediately load the user attached to the token
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task<IEnumerable<RefreshToken>> GetTokensByUserIdAsync(Guid userId)
        {
            return await _dbSet
                .Where(rt => rt.UserID == userId)
                .ToListAsync();
        }
    }
}
