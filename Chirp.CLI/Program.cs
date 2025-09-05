using System;
using System.Globalization;
using System.Text;
using Chirp.CLI;
using CsvHelper;
using CsvHelper.Configuration;
using SimpleDB;



    
IDatabaseRepository<Cheep> database = new CSVDatabase<Cheep>();


var newCheep = new Cheep
{
    Author = Environment.UserName,
    Message = Console.ReadLine(),
    Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
};


database.Store(newCheep);
IEnumerable<Cheep> test = database.Read();


foreach (var cheep in test)
{
    Console.WriteLine($"{cheep.Author}: {cheep.Message} {convert(cheep.Timestamp)}");
}


string convert(long timestamp)
{
    DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
    string formatted = dto.ToLocalTime().ToString("MM/dd/yy HH:mm:ss");
    return formatted;
}