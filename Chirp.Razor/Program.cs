using Chirp.Razor.Data;
using Chirp.Razor;
using SQLitePCL;

var builder = WebApplication.CreateBuilder(args);

Batteries_V2.Init();
builder.Services.AddScoped<IDbFacade>(_ => new DBFacade());
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICheepService, CheepService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
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
