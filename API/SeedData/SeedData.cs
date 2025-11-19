using Data.Context;
using Data.Models;
using Data.Models.Trips;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;



namespace API.SeedData
{
    public static class SeedData
    {
        // تم تخفيض حجم الدفعة (BatchSize) إلى 100
        private const int BatchSize = 100;

        public static async Task Seed(ApplicationDbContext context)
        {
            try
            {
                var basePath = Directory.GetCurrentDirectory();
                var seedDataPath = Path.Combine(basePath, "SeedData");

                var jsonOptions = new JsonSerializerOptions
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString,
                    PropertyNameCaseInsensitive = true
                };


                if (!await context.Classes.AnyAsync())
                {
                    Console.WriteLine("Starting Classes Seeding...");
                    await BatchSeedFile<Class>(context, seedDataPath, "Classes.json", context.Classes, "Classes", jsonOptions);
                    Console.WriteLine("Classes seeded successfully.");
                }
                else { Console.WriteLine("Classes table already contains data. Skipping."); }

                // TrainCoaches لديه مفتاح مركب (TrainID, Coach_ID)، يجب ضمان عدم التكرار يدوياً عبر GroupBy.
                if (!await context.TrainCoaches.AnyAsync())
                {
                    Console.WriteLine("Starting TrainCoaches Seeding...");

                    var trainCoaches = await LoadAndDeserializeAsync<TrainCoach>(seedDataPath, "TrainCoach.json", jsonOptions);

                    // تطبيق منطق ضمان التفرد باستخدام المفتاح المركب
                    var distinctTrainCoaches = trainCoaches
                        .GroupBy(tc => new { tc.TrainID, tc.Coach_ID }) // التجميع حسب المفتاح المركب
                        .Select(g => g.First())                      
                        .ToList();

                    await BatchSeedList<TrainCoach>(context, distinctTrainCoaches, context.TrainCoaches, "TrainCoaches");

                    Console.WriteLine("TrainCoaches seeded successfully.");
                }
                else { Console.WriteLine("TrainCoaches table already contains data. Skipping."); }

                if (!await context.Coaches.AnyAsync())
                {
                    Console.WriteLine("Starting Coaches Seeding...");
                    await BatchSeedFile<Coach>(context, seedDataPath, "Coaches.json", context.Coaches, "Coaches", jsonOptions);
                    Console.WriteLine("Coaches seeded successfully.");
                }
                else { Console.WriteLine("Coaches table already contains data. Skipping."); }

                if (!await context.Trains.AnyAsync())
                {
                    Console.WriteLine("Starting Trains Seeding...");
                    await BatchSeedFile<Train>(context, seedDataPath, "Train.json", context.Trains, "Trains", jsonOptions);
                    Console.WriteLine("Trains seeded successfully.");
                }
                else { Console.WriteLine("Trains table already contains data. Skipping."); }

                if (!await context.Stations.AnyAsync())
                {
                    Console.WriteLine("Starting Stations Seeding...");
                    var stations = await LoadAndDeserializeAsync<Station>(seedDataPath, "Stations.json", jsonOptions);
                    var distinctStations = stations
                        .GroupBy(s => s.StationID)
                        .Select(g => g.First())
                        .ToList();
                    // استخدام دالة الحفظ الجديدة
                    await BatchSeedList<Station>(context, distinctStations, context.Stations, "Stations");
                    Console.WriteLine($"Stations seeded successfully. Count: {distinctStations.Count}");
                }
                else { Console.WriteLine("Stations table already contains data. Skipping."); }

                context.ChangeTracker.Clear();
                Console.WriteLine("Change tracker cleared before Trips seeding.");


                List<Trip> tripsToSeed = new List<Trip>();

                // ------------------------------------
                // جزء التغذية لـ (Trips)
                // ------------------------------------
                if (!await context.Trips.AnyAsync())
                {
                    Console.WriteLine("Starting Trips Seeding (Step 1/3: Saving Trips)...");
                    tripsToSeed = await LoadAndDeserializeAsync<Trip>(seedDataPath, "Trip.json", jsonOptions);

                    // حفظ الرحلات أولاً
                    await BatchSeedList<Trip>(context, tripsToSeed, context.Trips, "Trips");
                    Console.WriteLine($"Trips seeded successfully. Count: {tripsToSeed.Count}");
                }
                else
                {
                    Console.WriteLine("Trips table already contains data. Skipping seeding, but loading existing IDs for TripStops linking.");
                    // إذا كانت البيانات موجودة، نقوم بتحميلها لأننا سنحتاجها في الخطوة التالية لتحديث TripID في TripStops
                    tripsToSeed = await context.Trips.OrderBy(t => t.TripID).ToListAsync();
                }

                context.ChangeTracker.Clear();

                // ------------------------------------
                // جزء التغذية لـ (TripStops) - يعتمد على IDs من Trips
                // ------------------------------------
                if (!await context.TripStops.AnyAsync() && tripsToSeed.Any())
                {
                    Console.WriteLine("Starting TripStops Seeding (Step 2/3: Linking Stops & Enforcing Uniqueness)...");

                    // تأكد من أن لدينا بيانات Stations قبل المتابعة، أو أن البيانات موجودة في قاعدة البيانات
                    if (!await context.Stations.AnyAsync())
                    {
                        // يفضل أن يتم التأكد من أن Stations محملة مسبقًا
                        Console.WriteLine("Error: Cannot seed TripStops because Stations table is empty. Please ensure Stations are seeded first.");
                    }

                    var allTripStopsData = await LoadAndDeserializeAsync<TripStop>(seedDataPath, "TripStop.json", jsonOptions);

                    // التعديل: ضمان تفرد محطات التوقف داخل كل رحلة باستخدام (TripID, StopOrder)
                    var distinctTripStops = allTripStopsData
                        .GroupBy(ts => new { ts.TripID, ts.StopSequence })
                        .Select(g => g.First()) // اختيار أول عنصر من كل مجموعة (لضمان التفرد)
                        .ToList();

                    // التجميع بالرقم التعريفي القديم للرحلة (الموجود في ملف JSON)
                    var stopsLookup = distinctTripStops
                        .GroupBy(ts => ts.TripID)
                        .ToDictionary(g => g.Key, g => g.ToList());

                    List<TripStop> finalStops = new List<TripStop>();

                    // تعديل الـ TripID بناءً على الـ IDs الجديدة المُنشأة في قاعدة البيانات
                    for (int i = 0; i < tripsToSeed.Count; i++)
                    {
                        var newTrip = tripsToSeed[i];
                        // افتراض أن الرقم التعريفي القديم (في ملف JSON) كان يبدأ من 1 ويتزايد
                        int oldHardcodedTripId = i + 1;

                        if (stopsLookup.TryGetValue(oldHardcodedTripId, out var stopsForThisTrip))
                        {
                            foreach (var stop in stopsForThisTrip)
                            {
                                // تحديث الرقم التعريفي للرحلة بالرقم الجديد المُنشأ
                                stop.TripID = newTrip.TripID;
                                finalStops.Add(stop);
                            }
                        }
                    }

                    // استخدام الدالة الجديدة للحفظ على دفعات
                    await BatchSeedList<TripStop>(context, finalStops, context.TripStops, "TripStops");

                    Console.WriteLine("TripStops seeded successfully.");
                    Console.WriteLine($"Total distinct TripStops saved: {finalStops.Count}");

                }
                else if (!tripsToSeed.Any())
                {
                    Console.WriteLine("TripStops seeding skipped. Cannot seed TripStops because Trips table is empty.");
                }
                else
                {
                    Console.WriteLine("TripStops table already contains data. Skipping.");
                }

                context.ChangeTracker.Clear();


                if (!await context.Seats.AnyAsync())
                {
                    Console.WriteLine("Starting Seats Seeding...");
                    await BatchSeedFile<Seat>(context, seedDataPath, "Seat.json", context.Seats, "Seats", jsonOptions);
                    Console.WriteLine("Seats seeded successfully.");
                }
                else { Console.WriteLine("Seats table already contains data. Skipping."); }

                if (!await context.TripSegmentPrices.AnyAsync())
                {
                    Console.WriteLine("Starting TripSegmentPrices Seeding...");
                    await BatchSeedFile<TripSegmentPrice>(context, seedDataPath, "TripSegmentPrice.json", context.TripSegmentPrices, "TripSegmentPrices", jsonOptions);
                    Console.WriteLine("TripSegmentPrices seeded successfully.");
                }
                else { Console.WriteLine("TripSegmentPrices table already contains data. Skipping."); }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during database migration or seeding. Error: {ex.Message}");
                throw new Exception("Seeding failed due to a file, deserialization, or batching error.", ex);
            }
        }

        /// <summary>
        /// دالة مساعدة لتحميل وفك تسلسل ملف JSON صغير إلى قائمة.
        /// </summary>
        private static async Task<List<T>> LoadAndDeserializeAsync<T>(string seedDataPath, string fileName, JsonSerializerOptions jsonOptions) where T : class
        {
            var filePath = Path.Combine(seedDataPath, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: Seed file not found at {filePath}");
                return new List<T>();
            }

            try
            {
                using var stream = File.OpenRead(filePath);
                return await JsonSerializer.DeserializeAsync<List<T>>(stream, jsonOptions) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deserializing {fileName}: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// دالة مساعدة للتغذية على دفعات (Batch Seeding) لـ List&lt;T&gt; لتجنب مشاكل التتبع والحفظ.
        /// يتم تقسيم القائمة وحفظها على دفعات مع مسح التتبع بعد كل دفعة.
        /// </summary>
        public static async Task BatchSeedList<T>(ApplicationDbContext context, List<T> entities, DbSet<T> dbSet, string entityName) where T : class
        {
            if (entities == null || !entities.Any())
            {
                Console.WriteLine($"Warning: No entities found for {entityName}. Skipping batch seed.");
                return;
            }

            int totalCount = entities.Count;
            Console.WriteLine($"Total entities to save for {entityName}: {totalCount}");

            for (int i = 0; i < totalCount; i += BatchSize)
            {
                var batch = entities.Skip(i).Take(BatchSize).ToList();
                dbSet.AddRange(batch);

                // الحفظ ومسح التتبع لضمان عدم تتبع الكيانات القديمة في الدفعة التالية
                await context.SaveChangesAsync();
                context.ChangeTracker.Clear();

                Console.WriteLine($"--- Saved batch up to index {Math.Min(i + BatchSize, totalCount)} of {totalCount} ({entityName}).");
            }
        }

        /// <summary>
        /// دالة مساعدة للتغذية على دفعات (Batch Seeding) من ملف JSON.
        /// تقوم بتحميل البيانات ثم تمريرها إلى دالة BatchSeedList للحفظ على دفعات.
        /// </summary>
        public static async Task BatchSeedFile<T>(ApplicationDbContext context, string seedDataPath, string fileName, DbSet<T> dbSet, string entityName, JsonSerializerOptions jsonOptions) where T : class
        {
            var filePath = Path.Combine(seedDataPath, fileName);

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: Seed file not found at {filePath}");
                return;
            }

            Console.WriteLine($"Starting batch seed for {entityName} from {fileName}...");

            // تحميل وفك التسلسل لجميع الكيانات
            var entities = await LoadAndDeserializeAsync<T>(seedDataPath, fileName, jsonOptions);

            // تمرير القائمة إلى دالة الحفظ على دفعات
            await BatchSeedList(context, entities, dbSet, entityName);

            Console.WriteLine($"Completed batch seed for {entityName}.");
        }
    }
}