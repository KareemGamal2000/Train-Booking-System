using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Models.Tickets
{
    public class TicketConfigration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
         
            builder.HasOne(t => t.Booking)
                   .WithMany(b => b.Tickets)
                   .HasForeignKey(t => t.Booking_ID)
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
