using Chirp.Domain;

using SQLitePCL;
using Microsoft.EntityFrameworkCore;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);

SQLitePCL.Batteries_V2.Init();

// Load database connection via configuration
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CheepDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<CheepRepo>();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<CheepDbContext>();
Console.WriteLine("YOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO");
Console.WriteLine(builder.Configuration["Authentication:GitHub:ClientId"]);
Console.WriteLine(builder.Configuration["Authentication:GitHub:ClientId"]);
Console.WriteLine(builder.Configuration["Authentication:GitHub:ClientId"]);
Console.WriteLine(builder.Configuration["Authentication:GitHub:ClientSecret"]);


builder.Services.AddAuthentication()
    .AddGitHub(options =>
    {
        options.ClientId = builder.Configuration["Authentication:GitHub:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
        options.Scope.Add("user:email");
        options.SaveTokens = true;
    });


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
