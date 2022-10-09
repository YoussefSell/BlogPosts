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
    options.UseSqlite("Data Source=database.db");
});

var app = builder.Build();

app.UseHttpsRedirection();

// middleware used to read & set tenant info
app.UseMiddleware<TenantMiddleware>();

app.MapGet("/books", async (TenantDbContext context) =>
{
    return await context.Query<Book>().ToListAsync();
});

app.Run();