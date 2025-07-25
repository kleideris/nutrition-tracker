using Microsoft.EntityFrameworkCore;
using NutritionTracker.Api.Data;

namespace NutritionTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //-------------------------------------------
            // adds db connection string from env variables
            builder.Services.AddDbContext<NutritionTrackerDBContext>(
                options => options.UseSqlServer(
                    builder
                    .Configuration
                    .GetConnectionString("DefaultConnection")
                )

            );
            //-------------------------------------------

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
