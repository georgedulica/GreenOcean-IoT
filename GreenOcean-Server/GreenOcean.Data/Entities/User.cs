namespace GreenOcean.Data.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public byte[] Password { get; set; }

    public byte[] Salt { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Code? Code { get; set; }

    public ICollection<Greenhouse> Greenhouses { get; } = new List<Greenhouse>(); 
}