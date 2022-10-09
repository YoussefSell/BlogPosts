namespace MultiTenant_DatabasePerTenant;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int MyProperty { get; set; }

    public List<UserTenant> Tenants { get; set; }
}