using Chirp.Domain;
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
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<CheepDbContext>();

//oAuth 
builder.Services
    .AddAuthentication(options =>
    {
        options.RequireAuthenticatedSignIn = true;
    })
    .AddGitHub(options =>
    {
        options.ClientId = githubClientId; // GitHub Client ID
        options.ClientSecret = githubClientSecret; // GitHub Client secret
    });


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
    DbInitializer.SeedDatabase(db);            
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
