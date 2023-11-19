using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using VTVApp.Api;
using VTVApp.Api.Data;
using VTVApp.Api.Models.DTOs.Users;
using VTVApp.Api.Models.Entities;
using VTVApp.Api.Services.Interfaces;

namespace VTVApp.SpecflowTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("specflow.json")
                    .Build();

                var connectionString = config.GetConnectionString("DefaultConnection");

                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<VTVDataContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add a database context using in-memory database
                services.AddDbContext<VTVDataContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Mock services
                var mockTokenService = Substitute.For<ITokenService>();
                var mockPasswordHashingService = Substitute.For<IPasswordHashingService>();

                // Configure behavior of mock services as needed
                mockTokenService.CreateToken(Arg.Any<UserDto>()).Returns("mockedToken");
                mockPasswordHashingService.HashPassword(Arg.Any<string>()).Returns("hashedPassword");
                mockPasswordHashingService.VerifyPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

                // Remove the existing implementations
                var tokenServiceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ITokenService));
                var passwordHashingServiceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IPasswordHashingService));
                if (tokenServiceDescriptor != null)
                {
                    services.Remove(tokenServiceDescriptor);
                }
                if (passwordHashingServiceDescriptor != null)
                {
                    services.Remove(passwordHashingServiceDescriptor);
                }

                // Add mock services to the service collection
                services.AddScoped<ITokenService>(_ => mockTokenService);
                services.AddScoped<IPasswordHashingService>(_ => mockPasswordHashingService);

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<VTVDataContext>();
                var logger = scopedServices
                    .GetRequiredService<ILogger<CustomWebApplicationFactory<Program>>>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    Utilities.InitializeDbForTests(db);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the " +
                        "database with test messages. Error: {Message}", ex.Message);
                }
            });
        }
    }
}
