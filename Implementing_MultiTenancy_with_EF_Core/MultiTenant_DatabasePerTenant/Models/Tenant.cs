namespace MultiTenant_DatabasePerTenant;

/// <summary>
/// this class used to hold the tenant informations
/// </summary>
public class Tenant
{
    public string Id { get; set; }

    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// when using a database per tenant this is the connection string to connect to the tenant database
    /// </summary>
    public string ConnectionString { get; set; }

    public List<UserTenant> Users { get; set; }
}
