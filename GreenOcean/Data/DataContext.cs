using GreenOcean.Entities;
using Microsoft.EntityFrameworkCore;
using RegisteredSystem = GreenOcean.Entities.System;

namespace GreenOcean.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Code> Codes { get; set; }

    public DbSet<Greenhouse> Greenhouses { get; set; }

    public DbSet<Plant> Plants { get; set; }

    public DbSet<IoTSystem> IoTSystems { get; set; }

    public DbSet<RegisteredSystem> Systems { get; set; }
}