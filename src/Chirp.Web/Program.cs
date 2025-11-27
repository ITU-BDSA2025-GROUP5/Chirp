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
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<User>(options =>
    options.SignIn.RequireConfirmedAccount = true)
.AddEntityFrameworkStores<CheepDbContext>();



var githubClientId = builder.Configuration["Authentication:GitHub:ClientId"];
var githubClientSecret = builder.Configuration["Authentication:GitHub:ClientSecret"];
Console.WriteLine(githubClientId);
Console.WriteLine(githubClientSecret);

if (!string.IsNullOrEmpty(githubClientId) && !string.IsNullOrEmpty(githubClientSecret))
{

builder.Services.AddAuthentication()
    .AddGitHub(options =>
    {
        options.ClientId = githubClientId;
        options.ClientSecret = githubClientSecret;
        options.Scope.Add("user:email");
        options.SaveTokens = true;
		options.CallbackPath = new PathString("/signin-github");


    });
}

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
