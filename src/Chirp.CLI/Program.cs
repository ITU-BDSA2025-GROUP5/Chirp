using System;
using Chirp.CLI;
using SimpleDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/cheeps", () => UserInterface.PrintCheeps(database.Read()));
app.MapPost("/cheep", (CheepInput input) =>
{
    var cheep = new Cheep { Author = Environment.UserName, Message = input.Message, Timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds() };
    database.Store(cheep);
    return Results.Created("/cheeps", cheep);
});

app.Run();

public record CheepInput(string Message);