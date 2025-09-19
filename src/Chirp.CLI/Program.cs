using System;
using Chirp.CLI;
using SimpleDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/cheeps", () => UserInterface.PrintCheeps(database.Read()));
app.MapPost("/cheeps", (string message) =>
{
    string author = Environment.UserName;
    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    Cheep cheep = new Cheep { Author = author, Message = message, Timestamp = timestamp };
    database.Store(cheep);
});
app.Run();
