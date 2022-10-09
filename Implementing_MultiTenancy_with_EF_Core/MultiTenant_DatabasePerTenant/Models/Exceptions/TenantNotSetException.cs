namespace MultiTenant_DatabasePerTenant;

[Serializable]
public class TenantNotSetException : Exception
{
    public TenantNotSetException() { }
    public TenantNotSetException(string message) : base(message) { }
    public TenantNotSetException(string message, Exception inner) : base(message, inner) { }
    protected TenantNotSetException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}