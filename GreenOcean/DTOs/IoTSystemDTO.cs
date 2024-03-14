namespace GreenOcean.DTOs;

public class IoTSystemDTO
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime Timestamp { get; set; }

    public string? Status { get; set; }

    public Guid GreenhouseId { get; set; }

    public Guid SystemId { get; set; }
}