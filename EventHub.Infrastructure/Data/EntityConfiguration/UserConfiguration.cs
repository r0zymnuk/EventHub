using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Infrastructure.Data.EntityConfiguration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        //builder.HasIndex(u => u.Email)
        //    .IsUnique();

        builder.Property(u => u.PhoneNumber)
            .HasMaxLength(15);

        builder.Property(u => u.ImageUrl);
        builder.HasMany(u => u.FavouriteCategories);

        builder.HasMany(u => u.EnteredEvents);

        builder.HasMany(u => u.Tickets);

        builder.HasMany(u => u.OrganizedEvents)
            .WithOne(e => e.Organizer);

        builder.HasMany(u => u.OrganizedTours)
            .WithOne(t => t.Organizer);
    }
}
