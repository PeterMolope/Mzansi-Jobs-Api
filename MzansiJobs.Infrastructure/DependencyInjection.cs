using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Infrastructure.Persistence;
using MzansiJobs.Infrastructure.Repositories;
using MzansiJobs.Infrastructure.Services;

namespace MzansiJobs.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString, npgsqlOptions =>
                {
                    // Ensures migrations assembly points to Infrastructure
                    npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                });
            });

            // Register Repositories and Unit of Work
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJobRepository, JobRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            // security services
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenService, TokenService>();



            return services;
        }
    }
}
