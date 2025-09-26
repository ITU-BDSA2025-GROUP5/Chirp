using System.Reflection.Metadata;
using Microsoft.Data.Sqlite;

namespace Chirp.Razor.Data;

public sealed class DBFacade : IDbFacade
{
    readonly QueryManager querymanager = new QueryManager();
    public int GetCheepCount()
    {
        var sqlDBFilePath = "/tmp/chirp.db";
        var sqlQuery = @"SELECT COUNT(*) FROM message;";

        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var count = reader.GetInt32(0);
                Console.WriteLine($"Message count: {count}");
                return count;
            }
        }
        return 0;
    }

    public List<CheepViewModel> GetCheeps()
    {
        var sqlDBFilePath = querymanager.GetFilePath();
        var sqlQuery = querymanager.GetShow50();
        var cheeps = new List<CheepViewModel>();
        using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = sqlQuery;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var author = reader.GetString(0);
                var message = reader.GetString(1);
                var timestamp = reader.GetDouble(2);
                var timestampString = UnixTimeStampToDateTimeString(timestamp);
                cheeps.Add(new CheepViewModel(author, message, timestampString));
            }
        }
        return cheeps;
    }
    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        var sqlDBFilePath = "/tmp/chirp.db";
        var sqlQuery = @"
            SELECT u.username AS author,
                m.text     AS message,
                CAST(m.pub_date AS REAL) AS ts
                FROM message m
                JOIN user   u ON u.user_id = m.author_id
                WHERE u.username = $author
                ORDER BY COALESCE(m.pub_date, 0) DESC
                LIMIT 50;";
        var list = new List<CheepViewModel>();
        using var connection = new SqliteConnection($"Data Source={querymanager.GetFilePath()}");
        connection.Open();
        using var cmd = connection.CreateCommand();
        cmd.CommandText = querymanager.GetSqlShowByAuthor();
        cmd.Parameters.AddWithValue("$author", author);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var a = reader.GetString(0);
            var m = reader.GetString(1);
            var timestamp = reader.GetDouble(2);
            list.Add(new CheepViewModel(a, m, UnixTimeStampToDateTimeString(timestamp)));
        }
        return list;
    }


    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }
    public string AddCheep(string username, string email, string pw_hash, int message_id, int author_id, string text, int pub_date)
    {
        return "Not implemented";
    }


    public class QueryManager
    {
        //querry manager kan tages ud til en seperat klasse hvis nødvendigt.

        const string SqlMessageSum = @"SELECT COUNT(*) FROM message;";
        const string FilePath = "/tmp/chirp.db";

        const string SqlShow50 = @"
        SELECT u.username AS author,
               m.text AS message,
               CAST(m.pub_date AS REAL) AS ts
        FROM message m
        JOIN user u ON u.user_id = m.author_id
        ORDER BY COALESCE(m.pub_date, 0) DESC
        LIMIT 50;";


        const string SqlShowByAuthor = @"
        SELECT u.username AS author,
               m.text AS message,
               CAST(m.pub_date AS REAL) AS ts
        FROM message m
        JOIN user u ON u.user_id = m.author_id
        WHERE u.username = $author
        ORDER BY COALESCE(m.pub_date, 0) DESC
        LIMIT 50;";

        public string GetMessageSum()
        {
            return SqlMessageSum;
        }

        public String GetFilePath()
        {
            return FilePath;
        }

        public String GetShow50()
        {
            return SqlShow50;
        }
        public String GetSqlShowByAuthor()
        {
            return SqlShowByAuthor;
        }

    }

}