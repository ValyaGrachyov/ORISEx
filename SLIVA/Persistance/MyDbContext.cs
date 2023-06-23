using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance;

public class MyDbContext : DbContext
{
    public DbSet<WeatherForecast> Weathers { get; set; }
    public DbSet<Author> Authors { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Weather>()
        //     .Property(b => b.Summary).IsRequired();
        //
        // modelBuilder.Entity<Weather>()
        //     .ToTable(t => t.HasCheckConstraint("Temp", "TemperatureC>-100 AND TemperatureC<80"));
    }
}