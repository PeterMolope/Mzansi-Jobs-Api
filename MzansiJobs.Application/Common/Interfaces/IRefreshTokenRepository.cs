using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Application.Common.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<IEnumerable<RefreshToken>> GetTokensByUserIdAsync(Guid userId);
    }
}
