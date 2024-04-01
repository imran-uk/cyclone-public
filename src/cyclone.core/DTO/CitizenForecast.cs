namespace cyclone.core.DTO;

public record CitizenForecast
{
    public int Id { get; set; }
    public string PlaceName { get; set; } = "Unknown";
    public double Temperature { get; set; }
    public DateTime Timestamp { get; set; }
}