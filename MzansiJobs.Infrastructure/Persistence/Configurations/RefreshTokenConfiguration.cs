using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Infrastructure.Persistence.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            // Primary Key
            builder.HasKey(rt => rt.ID);
            builder.Property(rt => rt.Token).IsRequired();
            builder.Property(rt => rt.createdAt).IsRequired();
            builder.Property(rt => rt.expiresOn).IsRequired();
            builder.Property(rt => rt.isRevoked).IsRequired();

            // builder.Property(rt => rt.IsActive).IsRequired();
            // builder.Property(rt => rt.IsExpired).IsRequired();

            builder.Ignore(rt => rt.IsActive);
            builder.Ignore(rt => rt.IsExpired);

            builder.Property(rt => rt.isUsed).IsRequired();

            // index token for fast lookups during refresh
            builder.HasIndex(rt => rt.Token);

            //many to one with User
            builder.HasOne(rt => rt.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.UserID)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
