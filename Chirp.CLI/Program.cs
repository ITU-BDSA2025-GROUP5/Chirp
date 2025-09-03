using System;
using System.Globalization;
using System.Text;

if (args[0] == "reed")
{
    String path = "C:\\Users\\marti\\Chirp\\Chirp.CLI\\chirp_cli_db.csv";
    string[] lines = File.ReadAllLines(path);
    String Author, Message, Timestamp;
        
    for (int i = 1; i < 5; i++)
    {
        string line = lines[i];
        string[] newline = line.Split('"');
        Message = newline[1];
        string[] newline2 = line.Split(',');
        Author = newline2[0];
        Timestamp = newline2[newline2.Length - 1];

        long unixSeconds = long.Parse(Timestamp);
        DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(unixSeconds);
        string formatted = dto.ToLocalTime().ToString("MM/dd/yy HH:mm:ss");
        Timestamp = formatted;

        Console.WriteLine(Author + " " + "@ " + formatted + ": " + Message);
    }
}

if (args[0] == "cheep")
{
    // niels Linux file path. skal rives og knækkes så den passer med de andres på en cool måde - enten en hjemmeside eller noget andet pjat -TA
    string filePath = "C:\\Users\\marti\\Chirp\\Chirp.CLI\\chirp_cli_db.csv";

    // https://www.influxdata.com/blog/current-time-c-guide/
    String Nowstring = DateTime.Now.ToString();

    String format = "MM-dd-yyyy HH:mm:ss"; //issue der kan være forskel på tidsformat på pc enten - eller / mellem mm dd yyyy
     
    List<String> linesToAdd = args.ToList();
    linesToAdd.Remove("cheep");
   
    String cheep = Stringformatter(linesToAdd);

    // fordi det ikke virker med engelsk/dansk tid skal vi lave det om til amerikansk tid 
    CultureInfo culture = new CultureInfo("en-US");

    DateTimeOffset dateTime = DateTimeOffset.ParseExact(Nowstring, format, culture);
    long date = dateTime.ToUnixTimeSeconds();
    String OSusername = Environment.UserName;
    
    // her laves en writer der skriver i dokumentet
    using (StreamWriter writer = new StreamWriter(filePath, append: true))
    {
        
        writer.WriteLine(OSusername + ",\"" + cheep + "\"," + date);
        Console.WriteLine("added to csv: " + OSusername + ", \"" + cheep + "\"," + date);

    }

}

// Bruges til at bygge cheep beskeden ud fra listen af strings
String Stringformatter(List<String> strings)
{
    StringBuilder Sb = new StringBuilder();
    
    Sb.AppendJoin(" ",strings);
        
    return Sb.ToString();
} 