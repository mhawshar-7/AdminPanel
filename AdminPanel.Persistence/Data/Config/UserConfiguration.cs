using AdminPanel.Data.Entities;
using AdminPanel.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Persistence.Data.Config
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
