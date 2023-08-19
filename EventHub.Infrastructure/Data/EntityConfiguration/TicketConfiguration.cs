using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Infrastructure.Data.EntityConfiguration;
public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(25);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(120);

        builder.OwnsMany(t => t.Features);

        builder.Navigation(t => t.Features)
            .AutoInclude();

        builder.Property(t => t.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(t => t.Quantity)
            .IsRequired();
    }
}   
