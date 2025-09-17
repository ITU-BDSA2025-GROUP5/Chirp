using System.Globalization;
using System.Runtime.InteropServices.Marshalling;
using CsvHelper;
using CsvHelper.Configuration;
using Chirp.CLI;
namespace SimpleDB;


public sealed class CSVDatabase<T> : IDatabaseRepository<T>
{
   private static CSVDatabase<T> instance = null;
   private static readonly object padlock = new object();
   
   private List<T> records;
   
   private string filepath = "data/chirp_cli_db.csv";
   private readonly CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
   {
      NewLine = Environment.NewLine,
   };

   private CSVDatabase()
   {
      
   }

   public static CSVDatabase<T> Instance
   {
      get
      {
         lock (padlock)
         {
            if (instance == null)
            {
               instance = new CSVDatabase();
            }
            return instance;
         }
      }
   }
   
   public IEnumerable<T> Read(int? limit = null)
   {
      using (var reader = new StreamReader(filepath))
      using (var csv = new CsvReader(reader, config))
      {
         records = csv.GetRecords<T>().ToList();
      }
      
      return records;
   }
   

public void Store(T record)
   {
      using (var writer = new StreamWriter(filepath, append: true))
      using (var csv = new CsvWriter(writer, config))
      {
         csv.NextRecord();
         csv.WriteRecord(record);
         writer.Flush();
      }
   }
}