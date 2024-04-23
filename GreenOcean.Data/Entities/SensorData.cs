namespace GreenOcean.Data.Entities;

public class SensorData
{
    public Guid Id { get; set; }

    public string Timestamp { get; set; }

    public float Humidity { get; set; }

    public float LightLevel { get; set; }

    public string SoilMoisture { get; set; }

    public float Temperature { get; set; }

    public Guid EquipmentId { get; set; }

    public Equipment Equipment { get; set; } = null!;
}