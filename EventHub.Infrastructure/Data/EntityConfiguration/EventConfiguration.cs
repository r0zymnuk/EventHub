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

        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.EndDate)
            .IsRequired();

        builder.Property(e => e.ImageUrl)
            .IsRequired();

        builder.HasOne(e => e.Organizer)
            .WithMany(u => u.OrganizedEvents);

        builder.OwnsOne(e => e.Location);

        builder.HasMany(e => e.Categories);

        builder.OwnsMany(e => e.Tickets);
    }
}
