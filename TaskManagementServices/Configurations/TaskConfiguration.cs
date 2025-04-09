using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementServices.Domain;

namespace TaskManagementServices.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskManagementServices.Domain.Task>
    {
        public void Configure(EntityTypeBuilder<TaskManagementServices.Domain.Task> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Description)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(t => t.DueDate)
                   .IsRequired();

            builder.Property(t => t.Status)
                   .HasConversion<int>()
                   .IsRequired();

            builder.HasOne<User>() 
                   .WithMany()
                   .HasForeignKey(t => t.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(t => t.User);
            builder.Ignore(t => t.Status);

        }
    }
}
