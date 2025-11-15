using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models.Trips
{
    public class TripConfigration : IEntityTypeConfiguration<Trip>
    {
        public void Configure(EntityTypeBuilder<Trip> builder)
        {
            builder.HasOne(t => t.Departure_Station)
                   .WithMany(s => s.DepartureTrips)
                   .HasForeignKey(t => t.DepartureStationID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Arrival_Station)
                   .WithMany(s => s.ArrivalTrips)
                   .HasForeignKey(t => t.ArrivalStationID)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(t => t.Stops)
                   .WithOne(ts => ts.Trip)
                   .HasForeignKey(ts => ts.TripID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.SegmentPrices)
                   .WithOne(tsp => tsp.Trip)
                   .HasForeignKey(tsp => tsp.TripID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
