using System.Collections.Generic;

public class AppDbContext : IdentityDbContext
{
    internal object Lecturers;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Claim> Claims { get; set; }
    public object Lecturer { get; internal set; }

    internal async Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
