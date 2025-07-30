using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Configuration;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.Repositories;

namespace NutritionTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Configuration to set up Entity Framework Core with SQL Server using a connection string
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");  // ConnString is pulled from User-Secrets for now
            builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connString));  // AddDbContext is scoped - per request, a new instance is created.

            // TODO: configure a way to add db connection string from the .env
            //var connString = Environment.GetEnvironmentVariable("DEFAULT_DB_CONNECTION");
            //Console.WriteLine($"Connection string: {connString}");

            // Automatically registers all AutoMapper profiles found in the same assembly as MapperConfig
            builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(MapperConfig)); });

            // Add UnitOfWork DI to the scope of IoC container
            builder.Services.AddRepositories();

            builder.Services.AddControllers();
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
