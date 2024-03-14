using Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);
        builder.HasIndex(u => u.UserId).IsUnique();
        builder.HasIndex(u => u.Login).IsUnique();
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.Fullname).IsRequired();
        builder.Property(u => u.Login).IsRequired();
        builder.Property(u => u.Email).IsRequired();
        builder.Property(u => u.PhoneNumber).IsRequired();
        builder.Property(u => u.UserId).IsRequired();
        builder.Property(u => u.RoleId).IsRequired().HasDefaultValue(new Guid("64141029-42fc-41f7-88eb-da0801efa3f3"));
        builder.HasOne<Role>(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
    }
}