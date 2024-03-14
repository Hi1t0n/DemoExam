using Demo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Infrastructure.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasKey(s => s.StatusId);
        builder.HasIndex(s => s.StatusId).IsUnique();
        builder.Property(s => s.StatusName).IsRequired();
    }
}