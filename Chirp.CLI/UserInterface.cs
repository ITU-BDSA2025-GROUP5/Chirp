using System;
using System.Collections.Generic;

namespace Chirp.CLI
{
    public static class UserInterface
    {
        public static void PrintCheeps(IEnumerable<Cheep> records)
        {
            foreach (var cheep in records)
            {
                Console.WriteLine($"{cheep.Author}: {cheep.Message} {convert(cheep.Timestamp)}");

            }
        }
        private static string convert(long timestamp)
        {
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            string formatted = dto.ToLocalTime().ToString("MM/dd/yy HH:mm:ss");
            return formatted;
        }

    }
}

