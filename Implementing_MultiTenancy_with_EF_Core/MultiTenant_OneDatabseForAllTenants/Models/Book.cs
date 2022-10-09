namespace MultiTenant_DatabasePerTenant;

public class Book : ITenantEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Tenant { get; set; } = default!;
}

/// <summary>
/// interface used to mark the model that are bound to specific tenants
/// </summary>
public interface ITenantEntity
{
    /// <summary>
    /// the if of the tenant
    /// </summary>
    public string Tenant { get; set; }
}
