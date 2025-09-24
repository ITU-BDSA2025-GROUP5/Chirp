using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleDB;
using Chirp.CLI; // only because Cheep is defined here – ideally move the model to a shared lib

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers(); // use AddControllersWithViews() if you have Razor views
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Initialize & register your CSV-backed repo (singleton)
var csvPath = Path.Combine(builder.Environment.ContentRootPath, "data", "chirp_cli_db.csv");
// If you implemented Init(path) on CSVDatabase<T>:
CSVDatabase<Cheep>.SetFilePath(csvPath);  
builder.Services.AddSingleton(CSVDatabase<Cheep>.Instance);
// If you prefer DI by interface, you can expose it as:
//// builder.Services.AddSingleton<IDatabaseRepository<Cheep>>(_ => CSVDatabase<Cheep>.Instance);

var app = builder.Build();

app.UseSwagger();

var showSwaggerUI = builder.Configuration.GetValue<bool>("Swagger:EnableUI", defaultValue: false);
if (showSwaggerUI || app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Chirp API v1");
    });
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseHttpsRedirection();
app.UseStaticFiles();     // keep if you serve static files (css/js/images)
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
