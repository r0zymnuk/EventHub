using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventHub.Infrastructure.Data.EntityConfiguration;
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(125);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(c => c.ImageUrl)
            .IsRequired();

        builder.HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories);

        builder.HasMany(c => c.Events)
            .WithMany(e => e.Categories);

        builder.HasMany(c => c.Tours)
            .WithMany(t => t.Categories);

        builder.HasMany(c => c.Users)
            .WithMany(u => u.FavouriteCategories);
    }
}
