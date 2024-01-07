using Iridium.Domain.Entities;
using Iridium.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iridium.Infrastructure.Configurations;

public class PasswordConfiguration : IEntityTypeConfiguration<Password>
{
    public void Configure(EntityTypeBuilder<Password> builder)
    {
        builder.Property(t => t.Username)
               .HasMaxLength(ConfigurationConstants.MAX_USERNAME_LENGTH)
               .IsRequired();
    }
}