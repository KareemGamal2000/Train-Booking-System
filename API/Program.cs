
using BLL.Interfaces;
using BLL.Mapping;
using BLL.Services;
using DAL.Context;
using DAL.Repo.Coach;
using DAL.Repo.Station;
using DAL.Repo.Train;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(opt=>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<ITrainRepo, TrainRepo>();
            builder.Services.AddScoped<ICoachRepo, CoachRepo>();
            builder.Services.AddScoped<IStationRepo, StationRepo>();

            // BLL
            builder.Services.AddScoped<ITrainService, TrainService>();
            builder.Services.AddScoped<ICoachService, CoachService>();
            builder.Services.AddScoped<IStationService, StationService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
