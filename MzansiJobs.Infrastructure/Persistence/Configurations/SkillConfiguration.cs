using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MzansiJobs.Domain.Entities;

namespace MzansiJobs.Infrastructure.Persistence.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {

        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.ToTable("Skills");

            // Primary Key
            builder.HasKey(s => s.ID);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(256);
            // index skill name  for fast lookup
            builder.HasIndex(s => s.Name)
                .IsUnique();

            //many to many jobSkills
            builder.HasMany(s => s.jobSkills)
                .WithOne(js => js.Skill)
                .HasForeignKey(js => js.SkillID)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
