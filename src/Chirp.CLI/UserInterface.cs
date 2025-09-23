using System;
using System.Collections.Generic;

namespace Chirp.CLI
{
    public static class UserInterface
    {
        public static string PrintCheeps(IEnumerable<Cheep> records)
        {
             return string.Join("\n", records.Select(cheep =>
                $"{cheep.Author}: {cheep.Message} {convert(cheep.Timestamp)}"));
        }
        private static string convert(long timestamp)
        {
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeSeconds(timestamp);
            string formatted = dto.ToLocalTime().ToString("MM/dd/yy HH:mm:ss");
            return formatted;
        }

    }
}

