using Microsoft.EntityFrameworkCore;
using Data.DB.CoordinatesDB;
using Repository.Repositories.Coordinates;
using Services.PointsService;

namespace SquaresAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<CoordinatesDBContext>(options =>
                     options.UseSqlServer(builder.Configuration.GetConnectionString("CoordinatesDBConnection") ?? 
                     throw new InvalidOperationException("Connection string 'CoordinatesDBConnection' not found.")));

            builder.Services.AddScoped<ICoordinatesRepository, CoordinatesRepository>();
            builder.Services.AddScoped<IPointsService, PointsService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<CoordinatesDBContext>();
                try
                {
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    // Log and handle the exception
                    Console.WriteLine($"An error occurred while creating the database: {ex.Message}");
                }
            }

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
