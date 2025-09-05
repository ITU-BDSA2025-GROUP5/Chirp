using System;
using System.Globalization;
using System.Text;
using Chirp.CLI;
using CsvHelper;
using CsvHelper.Configuration;

var config = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    NewLine = Environment.NewLine,
};

List<Cheep> records;

using (var reader = new StreamReader("/Users/tobiasnielsen/Chirp/Chirp.CLI/chirp_cli_db.csv"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    records = csv.GetRecords<Cheep>().ToList();
}

var newCheep = new Cheep
{
    Author = Environment.UserName,
    Message = Console.ReadLine(),
    Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
};
records.Add(newCheep);

using (var writer = new StreamWriter("/Users/tobiasnielsen/Chirp/Chirp.CLI/chirp_cli_db.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(records);
    writer.Flush();
}

foreach (var cheep in records)
{
    Console.WriteLine($"{cheep.Author}: {cheep.Message} {convert(cheep.Timestamp)}");
}
string convert(long timestamp)
{
    DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
    string formatted = dto.ToLocalTime().ToString("MM/dd/yy HH:mm:ss");
    return formatted;
}