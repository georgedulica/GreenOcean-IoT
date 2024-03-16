using Microsoft.Extensions.Hosting;
using RegisteredSystem = GreenOcean.Entities.System;

namespace GreenOcean.Entities;

public class IoTSystem
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime Timestamp {get; set;}

    public string? Status { get; set; }

    public Guid SystemId { get; set; }

    public RegisteredSystem System { get; set; } = null!;

    public Guid GreenhouseId { get; set; }

    public Greenhouse Greenhouse { get; set; } = null!;

    public ICollection<SensorData> SensorData { get; } = new List<SensorData>();
}