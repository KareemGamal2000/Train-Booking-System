using Data.EntityConfigrations;
using Data.Models;
using Data.Models.Tickets;
using Data.Models.Trips;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripStop> TripStops { get; set; }
        public DbSet<TripSegmentPrice> TripSegmentPrices { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Train> Trains { get; set; }

        public DbSet<TrainCoach> TrainCoaches { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TrainCoach>().HasKey(tc => new { tc.TrainID, tc.CoachID });

            builder.Entity<TrainCoach>().HasOne(tc => tc.Train).WithMany(t => t.TrainCoaches)
                   .HasForeignKey(tc => tc.TrainID)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TrainCoach>()
                  .HasOne(tc => tc.Coach)
                  .WithMany()
                  .HasForeignKey(tc => tc.CoachID)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Station>()
                  .Property(s => s.StationNameAR)
                  .HasAnnotation("SqlServer:IsFullTextIndexed", true)
                  .HasAnnotation("SqlServer:FullTextLanguage", "Arabic");
                  
            builder.Entity<Trip>()
                .HasOne(t => t.Train)
                .WithMany(t => t.Trips)
                .HasForeignKey(t => t.TrainID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(b => b.DepartureStop)
                .WithMany()
                .HasForeignKey(b => b.DepartureStopID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(b => b.ArrivalStop)
                .WithMany()
                .HasForeignKey(b => b.ArrivalStopID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}