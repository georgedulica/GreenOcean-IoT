using GreenOcean.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) {}

    public DbSet<User> Users { get; set; }

    public DbSet<Code> Codes { get; set; }

    public DbSet<Greenhouse> Greenhouses { get; set; }

    public DbSet<Plant> Plants { get; set; }

    public DbSet<Equipment> Equipment { get; set; }

    public DbSet<RegisteredEquipment> RegisteredEquipment { get; set; }

    public DbSet<Process> Processes { get; set; }
}