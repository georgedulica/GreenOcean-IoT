﻿using System.Reflection.Metadata;

namespace GreenOcean.Entities;

public class Greenhouse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Street { get; set; }

    public int? Number { get; set; }

    public string? City { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}