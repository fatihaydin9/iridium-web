using Iridium.Domain.Entities;
using Iridium.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iridium.Infrastructure.Configurations;

public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.Property(t => t.Name)
               .HasMaxLength(ConfigurationConstants.MaxWorkspaceLength)
               .IsRequired();
    }
}