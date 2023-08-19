using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Infrastructure.Data.EntityConfiguration;
public class TourConfiguration : IEntityTypeConfiguration<Tour>
{
    public void Configure(EntityTypeBuilder<Tour> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(125);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(t => t.ImageUrl)
            .IsRequired();

        builder.HasOne(t => t.Organizer)
            .WithMany(u => u.OrganizedTours);

        builder.HasMany(t => t.Events);

        builder.HasMany(t => t.Tickets);

        builder.OwnsMany(e => e.PromoCodes)
            .Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.OwnsMany(e => e.PromoCodes)
            .Property(p => p.Quantity)
            .IsRequired();

        builder.OwnsMany(e => e.PromoCodes)
            .Property(p => p.Discount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.UpdatedAt);
    }
}
