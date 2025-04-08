namespace GreenOcean.Business.DTOs;

public class ProcessDTO
{
    public Guid Id { get; set; }

    public string ProcessName { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime DueDate { get; set; }

    public Guid GreenhouseId { get; set; }
}