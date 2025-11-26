using Domain.Interfaces;
using Domain.Mapping;
using Domain.Services;
using Domain.Services.Auth;
using Domain.Third_Party.Token;
using Data.Context;
using Data.Models;
using Data.Repository.Coach;
using Data.Repository.Station;
using Data.Repository.Train;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Services.TrainService;
using Data.Repository.UnitOfWork;
using Domain.Services.StationService;
using Domain.Services.TripService;
using Data.Repository.Trip;

namespace API
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = false;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<JWT>(config.GetSection("JWT"));
            services.AddScoped(sp => sp.GetRequiredService<IOptions<JWT>>().Value);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = config["JWT:Issuer"],
                    ValidAudience = config["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]!))
                };
            });

            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<ITrainService, TrainService>();
            services.AddScoped<ITrainRepo, TrainRepo>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICoachRepo, CoachRepo>();
            services.AddScoped<IStationRepo, StationRepo>();
            services.AddScoped<ITripService, TripService>();
            services.AddScoped<ITripRepo, TripRepo>();
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<IStationService, StationService>();



            return services;
        }
    }
}
