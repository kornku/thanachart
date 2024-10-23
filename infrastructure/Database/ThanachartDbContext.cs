using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace infrastrucrue.Database;

public class ThanachartDbContext(DbContextOptions<ThanachartDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("th_TH");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
