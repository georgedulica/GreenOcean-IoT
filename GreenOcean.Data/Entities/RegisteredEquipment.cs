using System.Reflection.Metadata;

namespace GreenOcean.Data.Entities;

public class RegisteredEquipment
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public Equipment? Equipment { get; set; }
}