namespace GreenOcean.Business.DTOs;

public class GreenhouseDTO
{
    public Guid? Id { get; set; }

    public string Name { get; set; }

    public string? Street { get; set; }

    public int? Number { get; set; }

    public string? City { get; set; }
}