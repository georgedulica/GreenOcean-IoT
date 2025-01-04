using Amazon.DynamoDBv2.DataModel;

namespace GreenOcean.Data;

public class Data
{
    [DynamoDBHashKey("systemId")]
    public string SystemId { get; set; }


    [DynamoDBRangeKey("timestamp")]
    public string Timestamp { get; set; }


    [DynamoDBProperty("humidity")]
    public decimal Humidity { get; set; }


    [DynamoDBProperty("lightLevel")]
    public decimal LightLevel { get; set; }


    [DynamoDBProperty("soilMoisture")]
    public string SoilMoisture { get; set; }


    [DynamoDBProperty("temperature")]
    public decimal Temperature { get; set; }
}
