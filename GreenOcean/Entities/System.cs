using System.Reflection.Metadata;

namespace GreenOcean.Entities;

public class System
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime Timestamp {get; set;}

    public string? Status { get; set; }

    public Guid GreenhouseId { get; set; }

    public Greenhouse Greenhouse { get; set; } = null!;
}