using Microsoft.EntityFrameworkCore;
using MzansiJobs.Domain.Entities;
using System.Reflection;

namespace MzansiJobs.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Candidate> Candidates => Set<Candidate>();
        public DbSet<Employer> Employers => Set<Employer>();
        public DbSet<MzansiJobs.Domain.Entities.Application> Applications => Set<MzansiJobs.Domain.Entities.Application>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Skill> Skills => Set<Skill>();
        public DbSet<JobSkill> JobSkills => Set<JobSkill>();
        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<RefreshToken> refreshTokens => Set<RefreshToken>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder); // Calls base configuration first
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
