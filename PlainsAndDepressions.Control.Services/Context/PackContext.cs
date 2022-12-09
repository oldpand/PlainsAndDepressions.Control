using Microsoft.EntityFrameworkCore;
using PlainsAndDepressions.Control.Contracts.Data;

namespace PlainsAndDepressions.Control.Services.Context;

internal class PlainsAndDepressionsContext : DbContext
{
    public DbSet<Pack> Packs => Set<Pack>();
    public DbSet<Depression> Depressions => Set<Depression>();

    public PlainsAndDepressionsContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=OMEGA\\SQLEXPRESS;database=PlainsAndDepressions;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=True;") ;
    }
}
