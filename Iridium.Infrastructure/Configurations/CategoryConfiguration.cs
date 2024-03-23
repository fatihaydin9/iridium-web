using Iridium.Domain.Entities;
using Iridium.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Iridium.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(t => t.Name)
               .HasMaxLength(ConfigurationConstants.MaxCategoryLength)
               .IsRequired();
    }
}