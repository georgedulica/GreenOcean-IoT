namespace GreenOcean.Entities;

public class SensorData
{
    public Guid Id { get; set; }

    public string Timestamp { get; set; }

    public float Humidity { get; set; }

    public float LightLevel { get; set; }

    public string SoilMoisture { get; set; }

    public float Temperature { get; set; }

    public Guid IoTSystemId { get; set; }

    public IoTSystem IoTSystem { get; set; } = null!;
}