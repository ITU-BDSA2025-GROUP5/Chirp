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

using (var reader = new StreamReader("C:/Users/cfred/OneDrive/ITU/3. Semester/Analysis, Design and Software Architecture/Git/Chirp/Chirp/Chirp.CLI/chirp_cli_db.csv"))
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

using (var writer = new StreamWriter("C:/Users/cfred/OneDrive/ITU/3. Semester/Analysis, Design and Software Architecture/Git/Chirp/Chirp/Chirp.CLI/chirp_cli_db.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
    csv.WriteRecords(records);
    writer.Flush();
}

UserInterface.PrintCheeps(records);