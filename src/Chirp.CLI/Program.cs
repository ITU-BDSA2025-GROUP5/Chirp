using System;
using Chirp.CLI;
using SimpleDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();

var app = builder.Build();

app.MapGet("/cheeps", () => UserInterface.PrintCheeps(database.Read()));
app.Run();
