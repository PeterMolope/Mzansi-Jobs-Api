using Microsoft.EntityFrameworkCore;
using MzansiJobs.Application.Common.Interfaces;
using MzansiJobs.Domain.Entities;
using MzansiJobs.Infrastructure.Persistence;

namespace MzansiJobs.Infrastructure.Repositories
{
    public class CandidateRepository : GenericRepository<Candidate>, ICandidateRepository
    {
        private readonly AppDbContext _context;

        public CandidateRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Candidate?> GetByUserIdAsync(Guid userId)
        {
            return await _context.Set<Candidate>()
                .FirstOrDefaultAsync(c => c.UserID == userId);
        }
    }
}
