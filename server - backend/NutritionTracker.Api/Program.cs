using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NutritionTracker.Api.Configuration;
using NutritionTracker.Api.Core.Helpers;
using NutritionTracker.Api.Data;
using NutritionTracker.Api.Repositories;
using NutritionTracker.Api.Services;
using Serilog;
using System.Text;

namespace NutritionTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // This initializes a WebApplicationBuilder, which helps you configure services, middleware, and settings for your web application
            var builder = WebApplication.CreateBuilder(args);

            // Register Entity Framework Core with SQL Server context with DI using config connection string
            var connString = builder.Configuration.GetConnectionString("DefaultConnection");  // ConnString is pulled from User-Secrets for now
            builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connString));  // AddDbContext is scoped - per request, a new instance is created

            // TODO: configure a way to add db connection string from the .env
            //var connString = Environment.GetEnvironmentVariable("DEFAULT_DB_CONNECTION");
            //Console.WriteLine($"Connection string: {connString}");

            // AutoMapper setup. Automatically registers all AutoMapper profiles found in the same assembly as MapperConfig
            builder.Services.AddAutoMapper(cfg => { cfg.AddMaps(typeof(MapperConfig)); });

            // Add UnitOfWork DI to the scope of IoC container
            builder.Services.AddRepositories();

            // Add AplicationService DI to the scope of IoC container
            builder.Services.AddScoped<IApplicationService, ApplicationService>();


            // Sets up Serilog for structured logging. Reads config from appsettings.json
            builder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration);
            });

            
            // This configures JWT-based authentication using bearer tokens
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                var jwtSettings = builder.Configuration.GetSection("Authentication");
                options.IncludeErrorDetails = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = "https://codingfactory.aueb.gr",

                    ValidateAudience = false,
                    ValidAudience = "https://api.codingfactory.aueb.gr",

                    ValidateLifetime = true, // ensure not expired

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration["Authentication:SecretKey"]!))
                };
            });        

            // Enables Cross-Origin Resource Sharing so front-end clients can communicate with the API.
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll",  // unrestricted access (good for testing).
                    b => b.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());
            });
            builder.Services.AddCors(options => {
                options.AddPolicy("AngularClient",  // restricted to Angular dev server at port 4200.
                    b => b.WithOrigins("http://localhost:4200") // Assuming Angular runs on localhost:4200
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            // Registers controller support for MVC - style routing.
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>  // Makes Newtonsoft the default json serializer, replacing System.Text.Json
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    //options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            // Enables endpoint discovery
            builder.Services.AddEndpointsApiExplorer();

            // Configures Swagger UI and security schemes(JWT auth support)
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "NutritionTracker API", Version = "v1" });

                // Non-nullable reference are properly documented
                options.SupportNonNullableReferenceTypes();
                options.OperationFilter<AuthorizeOperationFilter>();
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme,
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        BearerFormat = "JWT"
                    });
                options.UseAllOfForInheritance();
            });

            // Newtonsoft support for Swagger
            builder.Services.AddSwaggerGenNewtonsoftSupport();

            //builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            // _Add services to the container.

            // Builds the app with all configured services and middleware
            var app = builder.Build();  // finalizes the DI container setup and constructs the application pipeline

            // _Configure the HTTP request pipeline.

            // In development mode, shows interactive Swagger documentation
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(/*c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "NutritionTracker API V1"); }*/);
            }

            // Forces HTTPS for all requests
            app.UseHttpsRedirection();

            // Applies CORS rules
            app.UseCors("AllowAll");
              
            // Applies Authentication middleware
            app.UseAuthentication();

            // Applies Authorization rules
            app.UseAuthorization();

            // Maps controller endpoints to route requests.
            app.MapControllers();

            // Starts the app and begins listening for incoming HTTP requests.
            app.Run();
        }
    }
}
