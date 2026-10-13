using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Infrastructure.Persistence.Configurations
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");

            // Primary Key
            builder.HasKey(c => c.ID);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(256);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(5000);
            builder.Property(c => c.Website).HasMaxLength(512);
            builder.Property(c => c.logoUrl).HasMaxLength(512);
            builder.Property(c => c.Industry).HasConversion<string>().HasMaxLength(50);
            builder.Property(c => c.Province).HasConversion<string>().HasMaxLength(100);

            //one to may with job
            builder.HasMany(c => c.jobs)
                .WithOne(j => j.Company)
                .HasForeignKey(j => j.CompanyID)
                .OnDelete(DeleteBehavior.Cascade);
            //one to many with employer
            builder.HasMany(c => c.employers)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyID)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
