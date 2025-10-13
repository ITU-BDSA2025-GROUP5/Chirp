using Chirp.Razor.Data;
using Chirp.Razor;
using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using Chirp.Razor.MessageRepository;


var builder = WebApplication.CreateBuilder(args);

SQLitePCL.Batteries_V2.Init();

// Load database connection via configuration
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CheepDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
  

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
