namespace MultiTenant_DatabasePerTenant;

public partial class CentralDbContext : DbContext
{
    public CentralDbContext(
        DbContextOptions<CentralDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; } = default!;

    public DbSet<Tenant> Tenants { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<UserTenant>()
                .HasKey(e => new { e.UserId, e.TenantId });

        modelBuilder
            .Entity<User>()
                .HasData(
                    new User
                    {
                        Id = "ABCED",
                        Name = "Test user",
                    }
                );

        modelBuilder
            .Entity<Tenant>()
                .HasData(
                    new Tenant
                    {
                        Id = "AZERT",
                        ConnectionString = "Data Source=tenant-AZERT-database.db"
                    }
                );

        modelBuilder
            .Entity<UserTenant>()
                .HasData(
                    new UserTenant
                    {
                        IsOwner = true,
                        UserId = "ABCED",
                        TenantId = "AZERT",
                        LassAccessAt = new DateTime(2022, 01, 01),
                    }
                );
    }
}