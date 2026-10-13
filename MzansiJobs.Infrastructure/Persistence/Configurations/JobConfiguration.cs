using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Infrastructure.Persistence.Configurations
{
    internal class JobConfiguration : IEntityTypeConfiguration<Job>
    {

        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.ToTable("Jobs");

            // Primary Key
            builder.HasKey(j => j.ID);
            builder.Property(j => j.Title).IsRequired().HasMaxLength(256);
            builder.Property(j => j.Description).IsRequired().HasMaxLength(5000);
            builder.Property(j => j.Province).HasConversion<string>().HasMaxLength(50);
            builder.Property(j => j.EmploymentType).HasConversion<string>().IsRequired().HasMaxLength(50);
            builder.Property(j => j.salaryRange).HasColumnType("decimal(18,2)");
            builder.Property(j => j.postedDate).IsRequired();
            builder.Property(j => j.expirationDate).IsRequired();
            builder.Property(j => j.isActive).IsRequired();

            //many-to-one relationship with Company
            builder.HasOne(j => j.Company)
                .WithMany(c => c.jobs)
                .HasForeignKey(j => j.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);
            //one to many relationship with Application
            builder.HasMany(j => j.applications)
                .WithOne(a => a.Job)
                .HasForeignKey(a => a.JobID)
                .OnDelete(DeleteBehavior.Cascade);
            // one to many relationship with JobSkill
            builder.HasMany(j => j.jobSkills)
                .WithOne(js => js.Job)
                .HasForeignKey(js => js.JobID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
