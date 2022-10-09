namespace MultiTenant_DatabasePerTenant;

public partial class TenantDbContext : DbContext
{
    private readonly ITenantProvider _tenantProvider;

    public TenantDbContext(
        ITenantProvider tenantProvider,
        DbContextOptions<TenantDbContext> options)
        : base(options)
    {
        _tenantProvider = tenantProvider;
    }

    public DbSet<Book> Books { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Book>()
                .HasData(
                    new Book
                    {
                        Id = 1,
                        Name = "book 1",
                        Tenant = "AZERT"
                    }
                );
    }

    /// <summary>
    /// apply a filter on the tenant id and return a IQueryable instance
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public IQueryable<TEntity> Query<TEntity>() where TEntity : class, ITenantEntity
        => Set<TEntity>().Where(e => e.Tenant == _tenantProvider.GetTenant().Id);
}
