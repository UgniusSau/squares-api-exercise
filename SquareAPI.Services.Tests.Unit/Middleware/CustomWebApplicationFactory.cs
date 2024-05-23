using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SquaresAPI;
using SquaresAPI.Middleware;
using System.Linq;
using Data.DB.CoordinatesDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Timeouts;

namespace SquareAPI.Services.Tests.Unit.Middleware
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's DbContext registration.
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CoordinatesDBContext>));
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Add DbContext using an in-memory database for testing.
                services.AddDbContext<CoordinatesDBContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database context (CoordinatesDBContext).
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<CoordinatesDBContext>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();
                }
            });

            builder.ConfigureWebHost(webHost =>
            {
                webHost.Configure(app =>
                {
                    app.UseRouting();
                    app.UseMiddleware<RequestTimeoutMiddleware>(5);
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });

            return base.CreateHost(builder);
        }
    }
}