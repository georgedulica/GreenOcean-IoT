namespace GreenOcean.Data.Entities;

public class Plant
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Soil { get; set; }

    public float? Height { get; set; }

    public string? Type { get; set; }

    public string? PhotoURL { get; set; }

    public float MositureLevel { get; set; }

    public float Humidity { get; set; }

    public float Temperature { get; set; }

    public string PhotoId { get; set; }

    public Guid GreenhouseId { get; set; }

    public Greenhouse Greenhouse { get; set; } = null!;
}