using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementServices.Domain;

namespace TaskManagementServices.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(100);

            // Unique non-clustered index on Email to prevent duplicates and improve search
            builder.HasIndex(u => u.Email)
                   .IsUnique()
                   .HasDatabaseName("IX_Users_Email_NonClustered");
        }
    }
}
