namespace GreenOcean.Business.DTOs;

public class PlantDTO
{
    public Guid? Id { get; set; }

    public string Name { get; set; }

    public string? Soil { get; set; }

    public float? Height { get; set; }

    public string? Type { get; set; }

    public string? PhotoURL { get; set; }

    public float? MositureLevel { get; set; }

    public float? Humidity { get; set; }

    public float? Temperature { get; set; }

    public Guid GreenhouseId { get; set; }
}