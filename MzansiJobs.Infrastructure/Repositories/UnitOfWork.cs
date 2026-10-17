using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Infrastructure.Persistence;

namespace MzansiJobs.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Dictionary<string, object>? _repositories;

        public IUserRepository Users { get; }
        public IJobRepository Jobs { get; }
        public IRefreshTokenRepository RefreshTokens { get; }

        public UnitOfWork(
            AppDbContext context,
            IUserRepository users,
            IJobRepository jobs,
            IRefreshTokenRepository refreshTokens)
        {
            _context = context;
            Users = users;
            Jobs = jobs;
            RefreshTokens = refreshTokens;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(TEntity)),
                    _context);

                _repositories.Add(type, repositoryInstance!);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
