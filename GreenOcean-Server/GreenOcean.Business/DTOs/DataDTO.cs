namespace GreenOcean.Business.DTOs;

public class DataDTO
{
    public string EquipmentId { get; set; }

    public string Timestamp { get; set; }

    public float Humidity { get; set; }

    public float LightLevel { get; set; }

    public string SoilMoisture { get; set; }

    public float Temperature { get; set; }

    public EquipmentDTO Equipment { get; set; }
}