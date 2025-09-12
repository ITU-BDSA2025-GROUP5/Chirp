using System.Globalization;
using System.Runtime.InteropServices.Marshalling;
using CsvHelper;
using CsvHelper.Configuration;
using Chirp.CLI;
namespace SimpleDB;


public sealed class CSVDatabase_Test<T> : IDatabaseRepository<T>
{
   private List<T> records;
   private readonly CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
   {
      NewLine = Environment.NewLine,
   };
   
   public IEnumerable<T> Read(int? limit = null)
   {
      using (var reader = new StreamReader("chirp_cli_db_test.csv"))
      using (var csv = new CsvReader(reader, config))
      {
         records = csv.GetRecords<T>().ToList();
      }
      
      return records;
   }
   

public void Store(T record)
   {
      using (var writer = new StreamWriter("chirp_cli_db_test.csv", append: true))
      using (var csv = new CsvWriter(writer, config))
      {
         csv.NextRecord();
         csv.WriteRecord(record);
         writer.Flush();
      }
   }
   
}