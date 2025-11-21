using Blazor_lab1.Components;
using Blazor_lab1.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

// Безопасное применение миграций
try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    Console.WriteLine("Checking database...");
    
    // Создаем базу если не существует (без миграций)
    await dbContext.Database.EnsureCreatedAsync();
    
    // Пытаемся применить миграции, если база уже существует
    if (await dbContext.Database.CanConnectAsync())
    {
        try
        {
            var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
            {
                Console.WriteLine($"Applying {pendingMigrations.Count()} migrations...");
                await dbContext.Database.MigrateAsync();
                Console.WriteLine("Migrations applied successfully.");
            }
        }
        catch (Exception migrationEx)
        {
            Console.WriteLine($"Migrations failed: {migrationEx.Message}");
            Console.WriteLine("Continuing with existing database structure...");
        }
    }
    
    Console.WriteLine("Database is ready.");
}
catch (Exception ex)
{
    Console.WriteLine($"Database initialization failed: {ex.Message}");
    Console.WriteLine("Application will continue without database.");
}

app.UseHttpsRedirection();
app.UseAntiforgery();

//endpoint для healthcheck
app.MapGet("/", () => "Blazor Application is running!");

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
