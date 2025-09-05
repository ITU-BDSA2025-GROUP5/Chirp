using System;
using System.Globalization;
using System.Text;
using Chirp.CLI;
using CsvHelper;
using CsvHelper.Configuration;
using DocoptNet;

const string usage = @"Chirp.
Usage:
  chirp cheep <message>
  chirp --version
Options:
  -h --help     Show this screen.
  --version     Show version.
";
var arguments = new Docopt().Apply(usage, args, version: "Chirp 1.0", exit: true);

var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    NewLine = Environment.NewLine
};

List<Cheep> records;
using (var reader = new StreamReader("/Users/oscardalsgaardjakobsen/Documents/GitHub/Chirp/Chirp.CLI/chirp_cli_db.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    records = csv.GetRecords<Cheep>().ToList();
}

if (arguments["cheep"].IsTrue)
{
    var newCheep = new Cheep
    {
        Author = Environment.UserName,
        Message = arguments["<message>"].ToString(),
        Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
    };
    records.Add(newCheep);
}

using (var writer = new StreamWriter("/Users/oscardalsgaardjakobsen/Documents/GitHub/Chirp/Chirp.CLI/chirp_cli_db.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(records);
    writer.Flush();
}

UserInterface.PrintCheeps(records);
