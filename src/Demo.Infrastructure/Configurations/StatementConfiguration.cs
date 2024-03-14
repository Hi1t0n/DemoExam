using Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Configurations;

public class StatementConfiguration : IEntityTypeConfiguration<Statement>
{
    public void Configure(EntityTypeBuilder<Statement> builder)
    {
        builder.HasKey(s => s.StatementId);
        builder.HasIndex(s => s.StatementId).IsUnique();
        builder.Property(s => s.UserId).IsRequired();
        builder.Property(s => s.StatementId).IsRequired();
        builder.Property(s => s.CarNumber).IsRequired()
            .HasMaxLength(9);
        builder.Property(s => s.Description).IsRequired();
        builder.Property(s => s.StatusId).IsRequired().HasDefaultValue(new Guid("0d47d368-e5d0-4e02-af24-8b7e5b372bd7"));
        builder.HasOne<User>(s => s.User)
            .WithMany(u => u.Statements)
            .HasForeignKey(s => s.UserId);
        builder.HasOne<Status>(s => s.Status)
            .WithMany(s => s.Statements)
            .HasForeignKey(s => s.StatusId);

    }
}