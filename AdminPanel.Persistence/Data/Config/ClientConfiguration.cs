using AdminPanel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminPanel.Persistence.Data.Config
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(c => c.Phone)
                   .HasMaxLength(20);

            builder.Property(c => c.Address)
                   .HasMaxLength(300);

            builder.Property(c => c.CreatedDate)
                   .IsRequired();

            builder.HasMany(c => c.Projects)
                   .WithOne(p => p.Client)
                   .HasForeignKey(p => p.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
