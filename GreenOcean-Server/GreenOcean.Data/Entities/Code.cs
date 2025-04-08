namespace GreenOcean.Data.Entities;

public class Code
{
    public Guid Id { get; set; }

    public int GeneratedCode { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}