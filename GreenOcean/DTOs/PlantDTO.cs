namespace GreenOcean.DTOs;

public class PlantDTO
{
    public Guid? Id { get; set; }

    public string Name { get; set; }

    public string? Soil { get; set; }

    public int? Height { get; set; }

    public string? Type { get; set; }

    public string? PhotoURL { get; set; }

    public int? MositureLevel { get; set; }

    public int? Humidity { get; set; }

    public int? MaxTemperature { get; set; }

    public int? MinTemperature { get; set; }

    public Guid GreenhouseId { get; set; }
}