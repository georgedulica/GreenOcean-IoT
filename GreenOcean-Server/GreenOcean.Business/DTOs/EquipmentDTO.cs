namespace GreenOcean.Business.DTOs;

public class EquipmentDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public float MaxTemperature { get; set; }

    public float MinTemperature { get; set; }

    public float MaxHumidity { get; set; }

    public float MinHumidity { get; set; }

    public float MaxLightLevel { get; set; }

    public float MinLightLevel { get; set; }

    public DateTime Timestamp { get; set; }

    public string Status { get; set; }

    public Guid GreenhouseId { get; set; }

    public Guid Code { get; set; }
}