namespace MultiTenant_DatabasePerTenant;

public class UserTenant
{
    public string UserId { get; set; }
    public User User { get; set; }
    
    public string TenantId { get; set; }
    public Tenant Tenant { get; set; }

    public bool IsOwner { get; set; }
    public DateTime LassAccessAt { get; set; }
}
