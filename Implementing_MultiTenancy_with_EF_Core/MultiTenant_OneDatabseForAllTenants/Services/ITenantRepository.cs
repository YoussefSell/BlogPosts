namespace MultiTenant_DatabasePerTenant;

public interface ITenantRepository
{
    Task<Tenant?> GetTenantAsync(string tenantId);
    Task<Tenant?> GetTenantAsync(string tenantId, string userId);
}

public class TenantRepository : ITenantRepository
{
    private readonly CentralDbContext _context;

    public TenantRepository(CentralDbContext context) => _context = context;

    public Task<Tenant?> GetTenantAsync(string tenantId)
        => _context.Tenants.FirstOrDefaultAsync(tenant => tenant.Id == tenantId);

    public Task<Tenant?> GetTenantAsync(string tenantId, string userId)
        => _context.Tenants.FirstOrDefaultAsync(tenant => tenant.Id == tenantId && tenant.Users.Any(t => t.UserId == userId));
}