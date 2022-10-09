namespace MultiTenant_DatabasePerTenant;

public interface ITenantProvider
{
    Tenant GetTenant();

    void SetTenant(Tenant tenant);
}

public class TenantProvider : ITenantProvider
{
    private Tenant? _tenant = null;

    public Tenant GetTenant()
    {
        return _tenant ?? throw new TenantNotSetException();
    }

    public void SetTenant(Tenant tenant)
    {
        _tenant = tenant;
    }
}
