using Chirp.Razor;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using Chirp.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

SQLitePCL.Batteries_V2.Init();

// Load database connection via configuration
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CheepDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<MessageRepo>(); 
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddRazorPages();
  

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CheepDbContext>();
    db.Database.Migrate();                     
    //DbInitializer.SeedDatabase(db);            
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
