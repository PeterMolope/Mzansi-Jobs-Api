namespace MzansiJobs.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IJobRepository Jobs { get; }
        IRefreshTokenRepository RefreshTokens { get; }

        // Generic fallback for simpler entities (like Skills, Company, etc.)
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> CompleteAsync();
    }
}
