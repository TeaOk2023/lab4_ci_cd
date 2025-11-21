// Замените блок try-catch на этот код:
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
