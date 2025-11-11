using Data.Context;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.SeedData
{
    public static class SeedData
    {
        public static void Seed()
        {
            using var context = new ApplicationDbContext();
            context.Database.EnsureCreated();

            string fileNameStations = @"../TeainBookingSystem.Data/SeedData/Stations.json";
            string jsonStringstations = File.ReadAllText(fileNameStations);
            var stations = JsonSerializer.Deserialize<List<Station>>(jsonStringstations);


            string fileNameClasses = @"../TeainBookingSystem.Data/SeedData/Classes.json";
            string jsonStringClasses = File.ReadAllText(fileNameClasses);
            var classes = JsonSerializer.Deserialize<List<Class>>(jsonStringClasses);


            context.Stations.AddRange(stations!);
            context.Classes.AddRange(classes!);
            context.SaveChanges();

        }
    }
}
