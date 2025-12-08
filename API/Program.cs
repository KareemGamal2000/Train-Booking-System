using API.SeedData;
using Data.Context;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            builder.Services.AddApplicationServices(builder.Configuration);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "أدخل JWT Token: Bearer {your-token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    logger.LogInformation("🔄 Starting database setup...");

                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                    var userManager = services.GetRequiredService<UserManager<User>>();

                    await context.Database.MigrateAsync();
                    logger.LogInformation("✅ Migrations applied");

                    await SeedData.SeedData.SeedRolesAsync(roleManager);
                    logger.LogInformation("✅ Roles seeded");

                    await SeedData.SeedData.SeedAdminUserAsync(userManager, roleManager);
                    logger.LogInformation("✅ Admin user seeded");

                    await SeedData.SeedData.Seed(context);
                    logger.LogInformation("✅ Data seeding completed");

                    // توليد المقاعد تلقائياً للعربات التي لا تحتوي على مقاعد
                    await SeedData.SeatGenerator.GenerateSeatsForCoachesAsync(context);
                    logger.LogInformation("✅ Seats generated for coaches");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "❌ Error during seeding: {Message}", ex.Message);
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
