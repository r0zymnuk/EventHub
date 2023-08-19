using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Infrastructure.Data.EntityConfiguration;
public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(125);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(e => e.Start)
            .IsRequired();

        builder.Property(e => e.End)
            .IsRequired();

        builder.Property(e => e.ImageUrl)
            .IsRequired();

        builder.HasOne(e => e.Organizer)
            .WithMany(u => u.OrganizedEvents);

        builder.OwnsOne(e => e.Location);

        builder.HasMany(e => e.Categories);

        builder.HasMany(e => e.Tickets);

        builder.Property(e => e.Currency)
            .IsRequired()
            .HasMaxLength(3)
            .IsFixedLength()
            .HasDefaultValue("USD");

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

        builder.Property(e => e.CreatedAt)
            .IsRequired();

        builder.Property(e => e.UpdatedAt);
    }
}
