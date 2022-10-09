var builder = WebApplication.CreateBuilder(args);

// register required services
builder.Services.AddScoped<TenantMiddleware>();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();
builder.Services.AddScoped<ITenantRepository, TenantRepository>();

// register the db contexts
builder.Services.AddDbContext<CentralDbContext>(options => {
    options.UseSqlite("Data Source=central-database.db");
});

builder.Services.AddDbContext<TenantDbContext>((serviceProvider, options) =>
{
    var tenant = serviceProvider.GetService<ITenantProvider>()?.GetTenant();
    if (tenant is null)
        throw new TenantNotSetException();

    // set the db context to the tenant connection string
    options.UseSqlite(tenant.ConnectionString);
});

var app = builder.Build();

app.UseHttpsRedirection();

// middleware used to read & set tenant info
app.UseMiddleware<TenantMiddleware>();

app.MapGet("/books", async (TenantDbContext context) =>
{
    return await context.Books.ToListAsync();
});

app.Run();