
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using VTVApp.Api.Data;
using VTVApp.Api.Repositories;
using VTVApp.Api.Repositories.Interfaces;
using NLog.Web;
using VTVApp.Api.Filters;
using VTVApp.Api.Services;
using VTVApp.Api.Services.Interfaces;

namespace VTVApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "VTVApp.API",
                    Description = "VTV API para el TP de Tecnicas Avanzadas de Programacion",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Support",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license")
                    }
                });
                options.CustomSchemaIds(type => type.FullName);
                options.OperationFilter<DateParameterOperationFilter>();
            });

            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddDbContext<VTVDataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins(allowedOrigins) // Replace with the port your React app is running on
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()); // Use this if your frontend sends credentials like cookies or basic auth
            });

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidateIssuer = true,
                        ValidateAudience = true
                    };
                });

            // Register Application Insights
            builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:InstrumentationKey"]);

            //// Configure NLog
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();  // This will apply NLog configuration

            // Register Scoped dependencies
            builder.Services.AddScoped<IVtvDataContext, VTVDataContext>();
            builder.Services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();
            builder.Services.AddScoped<ICheckpointRepository, CheckpointsRepository>();
            builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
            builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<IInspectionRepository, InspectionsRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors("AllowSpecificOrigin"); // Add this line to use the CORS policy

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}