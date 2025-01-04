using System.Reflection.Metadata;

namespace GreenOcean.Data.Entities;

public class Equipment
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public float MaxTemperature { get; set; }

    public float MinTemperature { get; set; }

    public float MaxHumidity { get; set; }

    public float MinHumidity { get; set; }

    public float MaxLightLevel { get; set; }

    public float MinLightLevel { get; set; }

    public DateTime Timestamp {get; set;}

    public string? Status { get; set; }

    public Guid GreenhouseId { get; set; }

    public Greenhouse Greenhouse { get; set; } = null!;

    public Guid RegisteredEquipmentId { get; set; }

    public RegisteredEquipment RegisteredEquipment { get; set; } = null!;
}