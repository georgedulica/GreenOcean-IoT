﻿namespace GreenOcean.Entities;

public class Process
{
    public Guid Id { get; set; }

    public string ProcessName { get; set; }

    public string Description { get; set; }

    public DateTime Timestamp { get; set; }

    public Guid GreenhouseId { get; set; }

    public Greenhouse Greenhouse { get; set; } = null!;
}