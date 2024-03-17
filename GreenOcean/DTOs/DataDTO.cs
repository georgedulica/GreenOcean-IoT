using Amazon.DynamoDBv2.DataModel;

namespace GreenOcean.DTOs;

public class DataDTO
{
    public string IoTSystemId { get; set; }

    public string Timestamp { get; set; }

    public float Humidity { get; set; }

    public float LightLevel { get; set; }

    public string SoilMoisture { get; set; }

    public float Temperature { get; set; }
}