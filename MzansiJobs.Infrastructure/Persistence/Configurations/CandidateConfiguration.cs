using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Infrastructure.Persistence.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("Candidates");

            // Primary Key
            builder.HasKey(c => c.ID);

            builder.Property(c => c.firstName).IsRequired().HasMaxLength(256);
            builder.Property(c => c.surname).IsRequired().HasMaxLength(256);
            builder.Property(c => c.CVUrl).HasMaxLength(512);
            builder.Property(c => c.phoneNumber).HasMaxLength(20);
            builder.Property(c => c.bio).HasMaxLength(512);

            //one-one relationship between Candidate and User
            builder.HasOne(c => c.User)
            .WithOne() // 
            .HasForeignKey<Candidate>(c => c.UserID)
            .OnDelete(DeleteBehavior.Cascade);

            //index for fast retrieval of candidate by user id
            builder.HasIndex(c => c.UserID)
            .IsUnique();

            // One-to-Many Relationship: Candidate -> Applications
            builder.HasMany(c => c.applications)
                .WithOne(ap => ap.Candidate)
                .HasForeignKey(ap => ap.CandidateID)
                .OnDelete(DeleteBehavior.Cascade);




        }

    }
}
