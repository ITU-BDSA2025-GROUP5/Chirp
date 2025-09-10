using System;
using System.Globalization;
using System.Text;
using Chirp.CLI;
using CsvHelper;
using CsvHelper.Configuration;
using SimpleDB;

using DocoptNet;

const string usage = @"Chirp.
Usage:
 chirp cheep <message>
 chirp read
 chirp --version
Options:
 -h --help     Show this screen.
--version     Show version.";

var arguments = new Docopt().Apply(usage, args, version: "Chirp 1.0", exit: true);

    
IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();


if (arguments["cheep"].IsTrue)
{
    var newCheep = new Cheep
    {
        Author = Environment.UserName,
        Message = arguments["<message>"].ToString(),
        Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
    };
    database.Store(newCheep);
}

if (arguments["read"].IsTrue) 
{ 
    UserInterface.PrintCheeps(database.Read());
}
