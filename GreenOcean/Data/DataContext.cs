using GreenOcean.Entities;
using Microsoft.EntityFrameworkCore;

namespace GreenOcean.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Code> Codes { get; set; }
}