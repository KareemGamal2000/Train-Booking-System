using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EntityConfigrations
{
    public class TicketConfigration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasOne(t => t.Trip)
                   .WithMany() 
                   .HasForeignKey(t => t.TripID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Booking)
                   .WithMany(b => b.Tickets)
                   .HasForeignKey(t => t.BookingID)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.Seat)
                   .WithOne()
                   .HasForeignKey<Ticket>(t => t.SeatID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Class)
                   .WithMany() 
                   .HasForeignKey(t => t.ClassID)
                   .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
