using RegisteredSystem = GreenOcean.Entities.System;

namespace GreenOcean.Entities;

public class IoTSystem
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

    public Guid SystemId { get; set; }

    public RegisteredSystem System { get; set; } = null!;

    public Guid GreenhouseId { get; set; }

    public Greenhouse Greenhouse { get; set; } = null!;

    public ICollection<SensorData> SensorData { get; } = new List<SensorData>();
}