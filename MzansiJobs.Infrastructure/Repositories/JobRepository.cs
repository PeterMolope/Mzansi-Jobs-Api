using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Domain.Entities;
using MzansiJobs.Domain.Enums;
using MzansiJobs.Infrastructure.Persistence;

namespace MzansiJobs.Infrastructure.Repositories
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Job>> SearchJobsAsync(string? keyword, Province? province)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(j => j.Title.Contains(keyword) || j.Description.Contains(keyword));
            }

            if (province.HasValue)
            {
                query = query.Where(j => j.Province == province.Value);
            }

            return await query
                .Include(j => j.Company)
                .ToListAsync();
        }

        public async Task<Job?> GetJobWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(j => j.Company)
                .Include(j => j.jobSkills) // Matches your lowercase property name
                    .ThenInclude(js => js.Skill)
                .FirstOrDefaultAsync(j => j.ID == id); // Matches your uppercase ID property
        }
    }
}
