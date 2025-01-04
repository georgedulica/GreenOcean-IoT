namespace GreenOcean.Data.Entities;

public class Process
{
    public Guid Id { get; set; }

    public string ProcessName { get; set; }

    public string Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime DueDate { get; set; }

    public Guid GreenhouseId { get; set; }

    public Greenhouse Greenhouse { get; set; } = null!;
}