using cyclone.core.DTO;

using Microsoft.EntityFrameworkCore;

namespace cyclone.infrastructure;

public class WeatherDatabaseContext : DbContext
{
    public WeatherDatabaseContext(DbContextOptions<WeatherDatabaseContext> options) : base(options)
    {
    }

    public DbSet<CitizenForecast> CitizenForecasts { get; set; } = null!;
}