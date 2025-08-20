using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
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
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;

namespace NutritionTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // This initializes a WebApplicationBuilder, which helps you configure services, middleware, and settings for your web application
            var builder = WebApplication.CreateBuilder(args);


            // Loads .env variables to configuration
            builder.Configuration.AddEnvSettings();


            // Register Entity Framework Core with SQL Server context with DI using config connection string
            var connString = builder.Configuration["ConnectionStrings:DefaultConnection"];
            builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connString));  // AddDbContext is scoped - per request, a new instance is created


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
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", // 👈 Add this line

                    ValidateIssuer = false,
                    ValidIssuer = "https://codingfactory.aueb.gr",

                    ValidateAudience = false,
                    ValidAudience = "https://api.codingfactory.aueb.gr",

                    ValidateLifetime = true, // ensure not expired

                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(jwtSettings["SecretKey"]!))
                };
            });        


            // Enables Cross-Origin Resource Sharing so front-end clients can communicate with the API.
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll",  // unrestricted access (good for testing).
                    b => b.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin());
            });
            //builder.Services.AddCors(options => {
            //    options.AddPolicy("AllowReactClient",  // restricted to React dev server at port 5173.
            //        b => b.WithOrigins("http://localhost:5173/") // Assuming React runs on localhost:5173
            //              .AllowAnyMethod()
            //              .AllowAnyHeader());
            //});
            //builder.Services.AddCors(options => {
            //    options.AddPolicy("AllowDockerClient",  // restricted to Docker composed frontend at port 3000.
            //        b => b.WithOrigins("http://localhost:3000/") // Assuming Docker frontend runs on localhost:3000
            //              .AllowAnyMethod()
            //              .AllowAnyHeader());
            //});


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

                // Show enums as strings
                options.SchemaGeneratorOptions = new SchemaGeneratorOptions { UseAllOfToExtendReferenceSchemas = false };
                
                // Allows Xlm comment support
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

            });


            // Newtonsoft support for Swagger
            builder.Services.AddSwaggerGenNewtonsoftSupport();


            // DI GlobalExceptionHandler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


            // Builds the app with all configured services and middleware
            var app = builder.Build();  // finalizes the DI container setup and constructs the application pipeline


            // Applies the error handler middleware
            app.UseMiddleware<ErrorHandlerMiddleware>();


            // Applies Global exception handler.
            app.UseExceptionHandler(options => { } );



            // Startup routine to prepare the database in Docker environments: - Applies migrations to initialize or update schema - Continues gracefully if DB already exists
            // - Seeds an admin user if missing
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();

                try
                {
                    Console.WriteLine("🛠️ Applying migrations...");
                    dbContext.Database.Migrate();
                    Console.WriteLine("✅ Migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("⚠️ Migration encountered an issue: " + ex.Message);
                    Console.WriteLine("ℹ️ Continuing startup — database may already exist or be partially initialized.");
                }

                //seeds an admin if none is present after migration attempt
                AdminSeeder.Seed(scope.ServiceProvider);
            }

            //In development mode, shows interactive Swagger documentation
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(/*c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "NutritionTracker API V1"); }*/);
            }

            // Applies CORS rules
            app.UseCors("AllowAll");

            // Redirects http requests to https    <---- commented out in dev since it requires a certificate to conenct with https
            //app.UseHttpsRedirection();


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
