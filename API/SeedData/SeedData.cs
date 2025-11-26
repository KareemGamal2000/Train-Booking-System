using Data.Context;
using Data.Models;
using Data.Models.Trips;
using Data.Models.Tickets;
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
        // تم إلغاء استخدام IDENTITY INSERT والاعتماد على آلية الربط في الذاكرة لضمان الاستقرار
        // يجب أن يكون حقل TripID في الـ Model مُجهزاً كـ Identity
        public static async Task Seed(ApplicationDbContext context)
        {
            try
            {
                var basePath = Directory.GetCurrentDirectory();
                var seedDataPath = Path.Combine(basePath, "SeedData");

                // مسار احتياطي للوصول إلى المجلد في بيئات مختلفة
                if (!Directory.Exists(seedDataPath))
                {
                    seedDataPath = Path.Combine(basePath, "..", "..", "..", "SeedData");
                    if (!Directory.Exists(seedDataPath))
                    {
                        Console.WriteLine($"Could not find SeedData folder at: {seedDataPath}");
                        return;
                    }
                }

                var jsonOptions = new JsonSerializerOptions
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString,
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip
                };

                // إضافة الـ Converters (لحل مشاكل أنواع البيانات الكبيرة)
                jsonOptions.Converters.Add(new DecimalJsonConverter());
                jsonOptions.Converters.Add(new NullableDecimalJsonConverter());
                jsonOptions.Converters.Add(new IntJsonConverter());
                jsonOptions.Converters.Add(new NullableIntJsonConverter());
                jsonOptions.Converters.Add(new LongJsonConverter());
                jsonOptions.Converters.Add(new NullableLongJsonConverter());
                jsonOptions.Converters.Add(new BooleanJsonConverter());
                jsonOptions.Converters.Add(new NullableBooleanJsonConverter());

                // ============================================================
                // 1. المحطات (Stations)
                // ============================================================
                context.ChangeTracker.Clear();
                if (!context.Stations.Any())
                {
                    var filePath = Path.Combine(seedDataPath, "Stations.json");
                    if (File.Exists(filePath))
                    {
                        var data = await File.ReadAllTextAsync(filePath);
                        var items = JsonSerializer.Deserialize<List<Station>>(data, jsonOptions);

                        if (items != null)
                        {
                            var uniqueItems = items.GroupBy(x => x.StationID).Select(g =>
                            {
                                var station = g.First();
                                if (string.IsNullOrWhiteSpace(station.StationNameEN))
                                    station.StationNameEN = !string.IsNullOrWhiteSpace(station.StationNameAR) ? station.StationNameAR : "Unknown";
                                if (string.IsNullOrWhiteSpace(station.StationNameAR))
                                    station.StationNameAR = station.StationNameEN ?? "Unknown";
                                return station;
                            }).ToList();

                            await context.Stations.AddRangeAsync(uniqueItems);
                            await context.SaveChangesAsync();
                            Console.WriteLine("Stations seeded.");
                        }
                    }
                }
                var validStationIds = new HashSet<long>(await context.Stations.Select(s => s.StationID).ToListAsync());


                // ============================================================
                // 2. الدرجات (Classes)
                // ============================================================
                context.ChangeTracker.Clear();
                if (!context.Classes.Any())
                {
                    var filePath = Path.Combine(seedDataPath, "Classes.json");
                    if (File.Exists(filePath))
                    {
                        var data = await File.ReadAllTextAsync(filePath);
                        var items = JsonSerializer.Deserialize<List<Class>>(data, jsonOptions);
                        if (items != null)
                        {
                            var uniqueItems = items.GroupBy(x => x.Class_ID).Select(g => g.First()).ToList();
                            await context.Classes.AddRangeAsync(uniqueItems);
                            await context.SaveChangesAsync();
                            Console.WriteLine("Classes seeded.");
                        }
                    }
                }

                var validClassIdSet = new HashSet<long>(await context.Classes.Select(c => c.Class_ID).ToListAsync());

                // ============================================================
                // 3. القطارات (Trains)
                // ============================================================
                context.ChangeTracker.Clear();
                if (!context.Trains.Any())
                {
                    var filePath = Path.Combine(seedDataPath, "Train.json");
                    if (File.Exists(filePath))
                    {
                        var data = await File.ReadAllTextAsync(filePath);
                        var items = JsonSerializer.Deserialize<List<Train>>(data, jsonOptions);
                        if (items != null)
                        {
                            var uniqueItems = items.GroupBy(x => x.TrainID).Select(g => g.First()).ToList();
                            await context.Trains.AddRangeAsync(uniqueItems);
                            await context.SaveChangesAsync();
                            Console.WriteLine("Trains seeded.");
                        }
                    }
                }
                var validTrainIdSet = new HashSet<long>(await context.Trains.Select(t => t.TrainID).ToListAsync());


                // ============================================================
                // 4. العربات (Coaches)
                // ============================================================
                context.ChangeTracker.Clear();
                if (!context.Coaches.Any())
                {
                    var filePath = Path.Combine(seedDataPath, "Coaches.json");
                    if (File.Exists(filePath))
                    {
                        var data = await File.ReadAllTextAsync(filePath);
                        var items = JsonSerializer.Deserialize<List<Coach>>(data, jsonOptions);
                        if (items != null)
                        {
                            var uniqueItems = items.GroupBy(x => x.Coach_ID).Select(g => g.First()).ToList();
                            await context.Coaches.AddRangeAsync(uniqueItems);
                            await context.SaveChangesAsync();
                            Console.WriteLine($"Coaches seeded.");
                        }
                    }
                }

                // ============================================================
                // 5. ربط القطارات بالعربات (TrainCoaches)
                // ============================================================
                context.ChangeTracker.Clear();
                if (!context.TrainCoaches.Any())
                {
                    var filePath = Path.Combine(seedDataPath, "TrainCoach.json");
                    if (File.Exists(filePath))
                    {
                        var data = await File.ReadAllTextAsync(filePath);
                        var items = JsonSerializer.Deserialize<List<TrainCoach>>(data, jsonOptions);

                        if (items != null)
                        {
                            // إزالة التكرارات للمفتاح المركب والتحقق من وجود الـ TrainID
                            var uniqueItems = items
                                .GroupBy(x => new { x.TrainID, x.Coach_ID })
                                .Select(g => g.First())
                                .Where(tc => validTrainIdSet.Contains(tc.TrainID))
                                .ToList();

                            await context.TrainCoaches.AddRangeAsync(uniqueItems);
                            await context.SaveChangesAsync();
                            Console.WriteLine("TrainCoaches seeded.");
                        }
                    }
                }

                // ============================================================
                // 6. الرحلات (Trips) - نستخدم TripID مباشرة من JSON
                // ============================================================
                context.ChangeTracker.Clear();
                if (!context.Trips.Any())
                {
                    var filePath = Path.Combine(seedDataPath, "Trip.json");
                    if (File.Exists(filePath))
                    {
                        var data = await File.ReadAllTextAsync(filePath);
                        // نستخدم الكلاس Trip الذي يحتوي على TripID
                        var sourceTrips = JsonSerializer.Deserialize<List<Trip>>(data, jsonOptions);

                        if (sourceTrips != null)
                        {
                            var tripsToSeed = new List<Trip>();
                            int skippedTrips = 0;

                            foreach (var trip in sourceTrips)
                            {
                                // التحقق من سلامة البيانات قبل الإضافة (Foreign Key Check)
                                bool isTrainValid = validTrainIdSet.Contains(trip.TrainID);
                                bool isDepStationValid = trip.DepartureStationID.HasValue && validStationIds.Contains(trip.DepartureStationID.Value);
                                bool isArrStationValid = trip.ArrivalStationID.HasValue && validStationIds.Contains(trip.ArrivalStationID.Value);

                                if (isTrainValid && isDepStationValid && isArrStationValid)
                                {
                                    // هنا نستخدم قيمة TripID من الـ JSON مباشرة
                                    tripsToSeed.Add(trip);
                                }
                                else
                                {
                                    skippedTrips++;
                                    // (تم إزالة رسائل التخطي لتجنب الإطالة في الإنتاج، لكنها مهمة للتصحيح)
                                }
                            }

                            if (tripsToSeed.Count > 0)
                            {
                                // يجب أن تكون الـ TripIDs في JSON فريدة ومخصصة
                                // نستخدم AddRangeAsync لإدخال القيم المحددة مسبقًا
                                await context.Trips.AddRangeAsync(tripsToSeed);
                                await context.SaveChangesAsync();
                                Console.WriteLine($"✅ Trips seeded: {tripsToSeed.Count} added, {skippedTrips} skipped");
                            }
                            else
                            {
                                Console.WriteLine("❌ ERROR: No valid trips to seed! Check your Trip.json file or Foreign Keys.");
                                return;
                            }
                        }
                    }
                }
                var validTripIdSet = new HashSet<int>(await context.Trips.Select(t => t.TripID).ToListAsync());

                // ============================================================
                // 7. وقفات الرحلات (TripStops) - نستخدم TripStopID و TripID مباشرة من JSON
                // ============================================================
                context.ChangeTracker.Clear();
                if (!context.TripStops.Any())
                {
                    var stopsFilePath = Path.Combine(seedDataPath, "TripStop.json");
                    if (File.Exists(stopsFilePath))
                    {
                        var stopsData = await File.ReadAllTextAsync(stopsFilePath);
                        var stopsItems = JsonSerializer.Deserialize<List<TripStop>>(stopsData, jsonOptions);

                        if (stopsItems != null)
                        {
                            var stopsToAdd = new List<TripStop>();
                            int skippedStops = 0;

                            foreach (var item in stopsItems)
                            {
                                // التحقق من وجود المفاتيح الخارجية
                                bool isTripValid = validTripIdSet.Contains(item.TripID);
                                bool isStationValid = item.StationID.HasValue && validStationIds.Contains(item.StationID.Value);

                                if (isTripValid && isStationValid)
                                {
                                    // هنا نستخدم TripStopID و TripID مباشرة من الـ JSON
                                    stopsToAdd.Add(item);
                                }
                                else
                                {
                                    skippedStops++;
                                }
                            }

                            if (stopsToAdd.Count > 0)
                            {
                                // يجب أن تكون الـ TripStopIDs في JSON فريدة ومخصصة
                                await context.TripStops.AddRangeAsync(stopsToAdd);
                                await context.SaveChangesAsync();
                                Console.WriteLine($"✅ TripStops seeded: {stopsToAdd.Count} added, {skippedStops} skipped");
                            }
                        }
                    }
                }
                var validTripStopIdSet = new HashSet<int>(await context.TripStops.Select(ts => ts.TripStopID).ToListAsync());


                // ============================================================
                // 8. أسعار الرحلات (TripSegmentPrices) - نستخدم SegmentPriceID و Stop IDs مباشرة من JSON
                // ============================================================
                context.ChangeTracker.Clear();
                if (!context.TripSegmentPrices.Any())
                {
                    var pricesFilePath = Path.Combine(seedDataPath, "TripSegmentPrice.json");
                    if (File.Exists(pricesFilePath))
                    {
                        var pricesData = await File.ReadAllTextAsync(pricesFilePath);
                        var pricesItems = JsonSerializer.Deserialize<List<TripSegmentPrice>>(pricesData, jsonOptions);

                        if (pricesItems != null)
                        {
                            var pricesToAdd = new List<TripSegmentPrice>();
                            int skippedPrices = 0;

                            foreach (var item in pricesItems)
                            {
                                // ********* تم التعديل هنا: استخدام validClassIdSet بدلاً من استعلام قاعدة البيانات *********
                                bool isClassValid = validClassIdSet.Contains(item.ClassID);
                                // *****************************************************************************************

                                bool isTripValid = validTripIdSet.Contains(item.TripID);
                                bool isStartStopValid = validTripStopIdSet.Contains(item.StartStopID);
                                bool isEndStopValid = validTripStopIdSet.Contains(item.EndStopID);

                                if (isClassValid && isTripValid && isStartStopValid && isEndStopValid)
                                {
                                    // هنا نستخدم SegmentPriceID وجميع IDs الأخرى مباشرة من الـ JSON
                                    pricesToAdd.Add(item);
                                }
                                else
                                {
                                    skippedPrices++;
                                    // (يمكن إضافة رسائل مفصلة هنا لغرض التصحيح)
                                }
                            }

                            if (pricesToAdd.Count > 0)
                            {
                                // يجب أن تكون الـ SegmentPriceIDs في JSON فريدة ومخصصة
                                await context.TripSegmentPrices.AddRangeAsync(pricesToAdd);
                                await context.SaveChangesAsync();
                            }

                            Console.WriteLine($"✅ TripSegmentPrices seeded: {pricesToAdd.Count} added, {skippedPrices} skipped");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CRITICAL ERROR during seeding: {ex.Message}");
                if (ex.InnerException != null)
                    Console.WriteLine($"Inner Error: {ex.InnerException.Message}");
            }
        }

        // ============================================================
        // Helper Classes for JSON Conversion (لم يتم تغييرها)
        // ============================================================

        public class DecimalJsonConverter : JsonConverter<decimal>
        {
            public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    if (decimal.TryParse(reader.GetString(), out decimal value)) return value;
                }
                if (reader.TokenType == JsonTokenType.Number) return reader.GetDecimal();
                return 0;
            }
            public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
        }

        public class NullableDecimalJsonConverter : JsonConverter<decimal?>
        {
            public override decimal? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    if (string.IsNullOrWhiteSpace(s)) return null;
                    if (decimal.TryParse(s, out decimal value)) return value;
                }
                if (reader.TokenType == JsonTokenType.Number) return reader.GetDecimal();
                return null;
            }
            public override void Write(Utf8JsonWriter writer, decimal? value, JsonSerializerOptions options)
            {
                if (value.HasValue) writer.WriteNumberValue(value.Value);
                else writer.WriteNullValue();
            }
        }

        public class IntJsonConverter : JsonConverter<int>
        {
            public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    if (int.TryParse(s, out int value)) return value;
                }
                if (reader.TokenType == JsonTokenType.Number) return reader.GetInt32();
                return 0;
            }
            public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
        }

        public class NullableIntJsonConverter : JsonConverter<int?>
        {
            public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    if (string.IsNullOrWhiteSpace(s)) return null;
                    if (int.TryParse(s, out int value)) return value;
                }
                if (reader.TokenType == JsonTokenType.Number) return reader.GetInt32();
                return null;
            }
            public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
            {
                if (value.HasValue) writer.WriteNumberValue(value.Value);
                else writer.WriteNullValue();
            }
        }

        public class BooleanJsonConverter : JsonConverter<bool>
        {
            public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.True) return true;
                if (reader.TokenType == JsonTokenType.False) return false;
                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    if (bool.TryParse(s, out var b)) return b;
                    if (s == "1") return true;
                    if (s == "0") return false;
                }
                if (reader.TokenType == JsonTokenType.Number) return reader.GetInt32() != 0;
                return false;
            }
            public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) => writer.WriteBooleanValue(value);
        }

        public class NullableBooleanJsonConverter : JsonConverter<bool?>
        {
            public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.Null) return null;
                if (reader.TokenType == JsonTokenType.True) return true;
                if (reader.TokenType == JsonTokenType.False) return false;
                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    if (string.IsNullOrWhiteSpace(s)) return null;
                    if (bool.TryParse(s, out var b)) return b;
                }
                return null;
            }
            public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
            {
                if (value.HasValue) writer.WriteBooleanValue(value.Value);
                else writer.WriteNullValue();
            }
        }

        public class LongJsonConverter : JsonConverter<long>
        {
            public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    if (long.TryParse(reader.GetString(), out long value)) return value;
                }
                if (reader.TokenType == JsonTokenType.Number) return reader.GetInt64();
                return 0;
            }
            public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options) => writer.WriteNumberValue(value);
        }

        public class NullableLongJsonConverter : JsonConverter<long?>
        {
            public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType == JsonTokenType.String)
                {
                    var s = reader.GetString();
                    if (string.IsNullOrWhiteSpace(s)) return null;
                    if (long.TryParse(s, out long value)) return value;
                }
                if (reader.TokenType == JsonTokenType.Number) return reader.GetInt64();
                return null;
            }
            public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options)
            {
                if (value.HasValue) writer.WriteNumberValue(value.Value);
                else writer.WriteNullValue();
            }
        }
    }
}