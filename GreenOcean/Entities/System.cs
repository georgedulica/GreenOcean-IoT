using System.Reflection.Metadata;
using IoTSystem = GreenOcean.Entities.IoTSystem;

namespace GreenOcean.Entities;

public class System
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public IoTSystem? IoTSystem { get; set; }
}