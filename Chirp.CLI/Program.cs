using System;
    {
        String path = "/Users/oscardalsgaardjakobsen/Documents/GitHub/Chirp/Chirp.CLI/chirp_cli_db.csv";
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