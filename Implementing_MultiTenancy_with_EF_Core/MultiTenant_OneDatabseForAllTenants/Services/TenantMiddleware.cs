namespace MultiTenant_DatabasePerTenant;

public class TenantMiddleware : IMiddleware
{
    private readonly ITenantProvider _tenantProvider;
    private readonly ITenantRepository _tenantRepository;

    public TenantMiddleware(ITenantProvider tenantProvider, ITenantRepository tenantRepository)
    {
        _tenantProvider = tenantProvider;
        _tenantRepository = tenantRepository;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // read the tenant id of the request
        var tenantId = GetTenantId(context);

        // ensure that the request contains the tenant id
        if (tenantId == null)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new { message = "you must supply a valid tenantId" });
            return;
        }

        // try to retrieve the tenant info
        var tenant = await _tenantRepository.GetTenantAsync(tenantId);
        if (tenant == null)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new { message = "you must supply a valid tenantId" });
            return;
        }

        // set the tenant info
        _tenantProvider.SetTenant(tenant);

        await next(context);
    }

    private static string? GetTenantId(HttpContext context)
    {
        // first we check if we have a header value for the tenant id
        if (context.Request.Headers.TryGetValue("tenant", out var headerValue))
            return headerValue.ToString();

        // then check if we have a query string value for the tenant id
        if (context.Request.Query.TryGetValue("tenant", out var queryValue))
            return queryValue.ToString();

        // tenant id is not specified
        return null;
    }
}