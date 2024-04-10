namespace GreenOcean.DTOs;

public class ProcessDTO
{
    public Guid Id { get; set; }

    public string ProcessName { get; set; }

    public string Description { get; set; }

    public DateTime Timestamp { get; set; }

    public Guid GreenhouseId { get; set; }
}