using AdminPanel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Persistence.Data.Config
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                   .HasMaxLength(1000);

            builder.Property(p => p.StartDate)
                   .IsRequired();


            builder.Property(p => p.EndDate)
                   .IsRequired(false);

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasDefaultValue(ProjectStatus.Pending);

            builder.Property(p => p.Budget)
                   .HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Client)
                   .WithMany(c => c.Projects)
                   .HasForeignKey(p => p.ClientId)
                   .IsRequired(false);
        }
    }
}
