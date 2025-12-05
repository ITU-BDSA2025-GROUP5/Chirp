using System.Data.Common;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; // Added for logging control

namespace Chirp.PlaywrightTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private IHost? _kestrelHost;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 1. SUPPRESS LOGGING (Crucial for performance)
            // This stops the console from being flooded with SQL commands
            services.AddLogging(logging => 
            {
                logging.ClearProviders(); 
            });

            // 2. Database Setup (In-Memory)
            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<CheepDbContext>));
            if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));
            if (dbConnectionDescriptor != null) services.Remove(dbConnectionDescriptor);

            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();
                return connection;
            });

            services.AddDbContext<CheepDbContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });
        });

        // 3. Build the "TestServer" Host (Internal Requirement)
        var testHost = builder.Build();

        // 4. Configure and Start the "Kestrel" Host (Real Server)
        builder.ConfigureWebHost(webHostBuilder =>
        {
            webHostBuilder.UseKestrel();
            webHostBuilder.UseUrls("http://127.0.0.1:0"); 
        });

        _kestrelHost = builder.Build();
        _kestrelHost.Start();

        // 5. Get the Port
        var server = _kestrelHost.Services.GetRequiredService<IServer>();
        var addresses = server.Features.Get<IServerAddressesFeature>();
        var kestrelUrl = addresses!.Addresses.First();

        ClientOptions.BaseAddress = new Uri(kestrelUrl);
        
        // 6. Initialize the Database
        using (var scope = _kestrelHost.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<CheepDbContext>();
            context.Database.Migrate(); 
        }

        // 7. Return the TestServer Host
        testHost.Start();
        return testHost;
    }

    protected override void Dispose(bool disposing)
    {
        _kestrelHost?.Dispose();
        base.Dispose(disposing);
    }
}