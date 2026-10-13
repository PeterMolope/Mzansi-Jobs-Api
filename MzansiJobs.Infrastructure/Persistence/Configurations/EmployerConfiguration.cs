using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Infrastructure.Persistence.Configurations
{
    internal class EmployerConfiguration : IEntityTypeConfiguration<Employer>
    {

        public void Configure(EntityTypeBuilder<Employer> builder)
        {
            builder.ToTable("Employers");

            // Primary Key
            builder.HasKey(e => e.ID);
            builder.Property(e => e.firstName).IsRequired().HasMaxLength(256);
            builder.Property(e => e.surname).IsRequired().HasMaxLength(256);
            builder.Property(e => e.phoneNumber).HasMaxLength(20);
            builder.Property(e => e.jobTitle).IsRequired().HasMaxLength(512);

            //one to one relationship with User
            builder.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Employer>(e => e.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // make sure suer ids are unique in the employer table
            builder.HasIndex(e => e.UserID)
                .IsUnique();
            //many to one relationship with Company
            builder.HasOne(e => e.Company)
                .WithMany(c => c.employers)
                .HasForeignKey(e => e.CompanyID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
