namespace GreenOcean.Data.Entities;

public class Greenhouse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Street { get; set; }

    public int? Number { get; set; }

    public string? City { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public ICollection<Plant> Plants { get; } = new List<Plant>();

    public ICollection<Equipment> Equipment { get; } = new List<Equipment>();

    public ICollection<Process> Processes { get; } = new List<Process>();
}