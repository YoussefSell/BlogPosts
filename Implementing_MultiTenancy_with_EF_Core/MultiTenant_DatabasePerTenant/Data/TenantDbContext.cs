namespace MultiTenant_DatabasePerTenant;

using Microsoft.EntityFrameworkCore.Design;

public partial class TenantDbContext : DbContext
{
    public TenantDbContext(DbContextOptions<TenantDbContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; } = default!;
}