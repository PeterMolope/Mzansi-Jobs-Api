using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            // Table Name
            builder.ToTable("Users");

            // Primary Key
            builder.HasKey(u => u.ID);

            // Properties & Constraints
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            // Ensure unique email addresses at the DB level
            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.Property(u => u.passwordHash)
                .IsRequired();

            // Enum stored as String in Database
            builder.Property(u => u.UserRole)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(u => u.dateCreated)
                .IsRequired();

            // One-to-Many Relationship: User -> RefreshTokens
            builder.HasMany(u => u.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
