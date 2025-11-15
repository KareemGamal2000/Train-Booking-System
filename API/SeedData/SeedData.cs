using Data.Context;
using Data.Models;
using Data.Models.Trips;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data.SeedData
{
    public static class SeedData
    {
        public static void Seed(ApplicationDbContext context)
        {

            try
            {
                var basePath = Directory.GetCurrentDirectory();
                var seedDataPath = Path.Combine(basePath, "SeedData");

                // إضافة خيارات Json مرنة
                var jsonOptions = new JsonSerializerOptions
                {
                    // هذا الخيار يسمح بقراءة الأرقام من تنسيق النص إذا كانت محاطة بعلامات اقتباس
                    NumberHandling = JsonNumberHandling.AllowReadingFromString,
                    PropertyNameCaseInsensitive = true // قد يساعد في تطابق أسماء الحقول
                };

                // قراءة ملف Stations.json
                string fileNameStations = Path.Combine(seedDataPath, "Stations.json");
                string jsonStringstations = File.ReadAllText(fileNameStations);
                var stations = JsonSerializer.Deserialize<List<Station>>(jsonStringstations, jsonOptions);

                // قراءة ملف Classes.json
                string fileNameClasses = Path.Combine(seedDataPath, "Classes.json");
                string jsonStringClasses = File.ReadAllText(fileNameClasses);
                var classes = JsonSerializer.Deserialize<List<Class>>(jsonStringClasses, jsonOptions);

                // قراءة ملف Coaches.json
                string fileNameCoaches = Path.Combine(seedDataPath, "Coaches.json");
                string jsonStringCoaches = File.ReadAllText(fileNameCoaches);
                var coaches = JsonSerializer.Deserialize<List<Coach>>(jsonStringCoaches, jsonOptions);

                // قراءة ملف Train.json
                string fileNameTrains = Path.Combine(seedDataPath, "Train.json");
                string jsonStringTrains = File.ReadAllText(fileNameTrains);
                var trains = JsonSerializer.Deserialize<List<Train>>(jsonStringTrains, jsonOptions);

                // قراءة ملف TrainCoach.json
                string fileNameTrainCoaches = Path.Combine(seedDataPath, "TrainCoach.json");
                string jsonStringTrainCoach = File.ReadAllText(fileNameTrainCoaches);
                // السطر 46 بعد التعديل:
                var traincoaches = JsonSerializer.Deserialize<List<TrainCoach>>(jsonStringTrainCoach, jsonOptions);

                // قراءة ملف Trip.json
                string fileNameTrips = Path.Combine(seedDataPath, "Trip.json");
                string jsonStringTrips = File.ReadAllText(fileNameTrips);
                var trips = JsonSerializer.Deserialize<List<Trip>>(jsonStringTrips, jsonOptions);

                // قراءة ملف TripStop.json
                string fileNameTripStops = Path.Combine(seedDataPath, "TripStop.json");
                string jsonStringTripStopss = File.ReadAllText(fileNameTripStops);
                var tripstopss = JsonSerializer.Deserialize<List<TripStop>>(jsonStringTripStopss, jsonOptions);

                // *** 3. الإضافة والحفظ في قاعدة البيانات ***
                context.Stations.AddRange(stations!);
                context.Classes.AddRange(classes!);
                context.Coaches.AddRange(coaches!);
                context.Trains.AddRange(trains!);
                context.TrainCoaches.AddRange(traincoaches!);
                context.Trips.AddRange(trips!);
                context.TripStops.AddRange(tripstopss!);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // إذا فشلت عملية الـ Seeding، سجل الخطأ بوضوح.
                // هذا الخطأ سيتم تسجيله بواسطة الـ Logger في Program.cs
                throw new Exception("Seeding failed due to a file or deserialization error.", ex);
            }
        }
    }
}