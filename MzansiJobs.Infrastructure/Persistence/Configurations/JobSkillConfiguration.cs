using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Infrastructure.Persistence.Configurations
{
    public class JobSkillConfiguration : IEntityTypeConfiguration<JobSkill>
    {
        public void Configure(EntityTypeBuilder<JobSkill> builder)
        {

            builder.ToTable("JobSkills");

            // Primary Key
            builder.HasKey(js => js.ID);

            //many to one with job
            builder.HasOne(js => js.Job)
                .WithMany(j => j.jobSkills)
                .HasForeignKey(js => js.JobID)
                .OnDelete(DeleteBehavior.Cascade);

            //many to one with skill
            builder.HasOne(js => js.Skill)
                .WithMany(s => s.jobSkills)
                .HasForeignKey(js => js.SkillID)
                .OnDelete(DeleteBehavior.Cascade);

            // Enforce uniqueness for Job-Skill pairs
            builder.HasIndex(js => new { js.JobID, js.SkillID })
                .IsUnique();

        }
    }
}
