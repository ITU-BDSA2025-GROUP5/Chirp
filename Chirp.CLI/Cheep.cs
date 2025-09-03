using System.Globalization;
using System.Text;

// todo figure out mulitple classes and how to call them using terminal
// figure out how to add mulitple lines to csv file, maybe combine them? 
// 


namespace Chirp.CLI;


// cheep klassen tilføjer cheeps til data filen/serveren/clouded/databasen
public class Cheep
{



    public static void Main(string[] args)
    {
        // niels Linux file path. skal rives og knækkes så den passer med de andres på en cool måde - enten en hjemmeside eller noget andet pjat -TA
        string filePath = "/mnt/c/ChirpFolder/chirper1st/Chirp/Chirp.CLI/chirp_cli_db.csv";




        // https://www.influxdata.com/blog/current-time-c-guide/
        String Nowstring = DateTime.Now.ToString();

        String format = "MM/dd/yyyy HH:mm:ss";
     
        List<String> linesToAdd = args.ToList();


      
        String Cheep = Stringformatter(linesToAdd);

        // fordi det ikke virker med engelsk/dansk tid skal vi lave det om til amerikansk tid 
        CultureInfo culture = new CultureInfo("en-US");

        DateTimeOffset dateTime = DateTimeOffset.ParseExact(Nowstring, format, culture);
        long date = dateTime.ToUnixTimeSeconds();
        String OSusername = Environment.UserName;
        // her laves en writer der skriver i dokumentet
        using (StreamWriter writer = new StreamWriter(filePath, append: true))
        {


            writer.WriteLine(OSusername + "," + Cheep + "," + date);
            Console.WriteLine("added to csv: " + OSusername + "," + Cheep + "," + date);

        }



    }

    // den her metode skal putte "," imellem alle strings og formatere controllere Cheeps som bliver lavet.
    // lige nu completly useless
    public static String Stringformatter(List<String> strings)
    {
        
        StringBuilder Sb = new StringBuilder();

        
        Sb.AppendJoin(" ",strings);
        
        return Sb.ToString();

    }
    
}

