using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MzansiJobs.Infrastructure.Persistence.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<MzansiJobs.Domain.Entities.Application>
    {
        public void Configure(EntityTypeBuilder<MzansiJobs.Domain.Entities.Application> builder)
        {
            builder.ToTable("Applications");

            // Primary Key
            builder.HasKey(a => a.ID);
            builder.Property(a => a.Status).IsRequired().HasConversion<string>().HasMaxLength(50);
            builder.Property(a => a.dateApplied).IsRequired().HasMaxLength(512);
            builder.Property(a => a.coverLetter).HasMaxLength(5000);

            //many to one relationship with Candidate
            builder.HasOne(a => a.Candidate)
            .WithMany(c => c.applications)
            .HasForeignKey(a => a.CandidateID)
            .OnDelete(DeleteBehavior.Cascade);

            //many to one relationship with Job
            builder.HasOne(a => a.Job)
            .WithMany(j => j.applications)
            .HasForeignKey(a => a.JobID)
            .OnDelete(DeleteBehavior.Cascade);

            //prevent multiple application to the same job by the same candidate
            builder.HasIndex(a => new { a.JobID, a.CandidateID })
            .IsUnique();


        }
    }
}
