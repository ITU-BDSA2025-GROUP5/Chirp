using System.Data.Common;
using Microsoft.Data.Sqlite;
namespace Chirp.Razor.Data;

public sealed class DBFacade : IDbFacade
{
    
    private readonly string _connectionString;

    public DBFacade(IConfiguration config)
    {
        // 1) Allow explicit env var path to WIN ALWAYS
        var envPath = Environment.GetEnvironmentVariable("CHIRPDBPATH");
        if (!string.IsNullOrWhiteSpace(envPath))
        {
            EnsureDirectoryExists(envPath);
            _connectionString = $"Data Source={envPath}";
            return;
        }

        // 2) Try ConnectionStrings from config (covers appsettings.json
        // AND Azure App Setting with key "ConnectionStrings:ChirpDb")
        var fromConnStr = config.GetConnectionString("ChirpDb");
        if (!string.IsNullOrWhiteSpace(fromConnStr))
        {
            _connectionString = fromConnStr;
            return;
        }

        // 3) Try Azure’s special env var prefixes (in case you used the
        // "Connection strings" blade with Type=Custom/SQL/SQLAzure)
        var azureConnStr =
            Environment.GetEnvironmentVariable("CUSTOMCONNSTR_ChirpDb")
            ?? Environment.GetEnvironmentVariable("SQLAZURECONNSTR_ChirpDb")
            ?? Environment.GetEnvironmentVariable("SQLCONNSTR_ChirpDb")
            ?? Environment.GetEnvironmentVariable("MYSQLCONNSTR_ChirpDb");

        if (!string.IsNullOrWhiteSpace(azureConnStr))
        {
            _connectionString = azureConnStr;
            return;
        }

        // 4) Fall back to a sane default per platform
        var defaultPath = GetDefaultDbPath();
        EnsureDirectoryExists(defaultPath);
        _connectionString = $"Data Source={defaultPath}";
    }

    private static string GetDefaultDbPath()
    {
        // Azure App Service (Linux) persisted volume
        if (OperatingSystem.IsLinux() && Directory.Exists("/home"))
            return "/home/site/chirp.db";

        // Local/dev fallback
        return Path.Combine(Path.GetTempPath(), "chirp.db");
    }

    private static void EnsureDirectoryExists(string filePath)
    {
        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }
    
    
    public int GetCheepCount()
    {
        var sqlDBFilePath = "/tmp/chirp.db";
        var sqlQuery = @"SELECT COUNT(*) FROM message;";
        try
        {
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

                return 1;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 1;
        }
    }

    public List<CheepViewModel> GetCheeps()
    {
        var sqlDBFilePath = "/tmp/chirp.db";
        var sqlQuery = @"
        SELECT  u.username AS author,
                m.text     AS message,
                CAST(m.pub_date AS REAL) AS ts
                FROM message m
                JOIN user   u ON u.user_id = m.author_id
                ORDER BY COALESCE(m.pub_date, 0) DESC";
        var cheeps = new List<CheepViewModel>();
        try
        {
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
                return cheeps;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var emptyList = new List<CheepViewModel>();
            return emptyList;
        }
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
                LIMIT 32;";
        var list = new List<CheepViewModel>();

        try
        {
            using var connection = new SqliteConnection($"Data Source={sqlDBFilePath}");
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sqlQuery;
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            var emptyList = new List<CheepViewModel>();
            return emptyList;
        }
    }

    public List<CheepViewModel> GetCheepsPage(int pageNumber, int pageSize)
    {
        var sqlDBFilePath = "/tmp/chirp.db";
        var offset = (pageNumber - 1) * pageSize;
        var sqlQuery = @"
        SELECT  u.username AS author,
                m.text     AS message,
                CAST(m.pub_date AS REAL) AS ts
                FROM message m
                JOIN user u ON u.user_id = m.author_id
                ORDER BY COALESCE(m.pub_date, 0) DESC
                LIMIT $pageSize OFFSET $offset;";
        var cheeps = new List<CheepViewModel>();

        try
        {
            using (var connection = new SqliteConnection($"Data Source={sqlDBFilePath}"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sqlQuery;
                command.Parameters.AddWithValue("$pageSize", pageSize);
                command.Parameters.AddWithValue("$offset", offset);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var author = reader.GetString(0);
                    var message = reader.GetString(1);
                    var timestamp = reader.GetDouble(2);
                    var timestampString = UnixTimeStampToDateTimeString(timestamp);
                    cheeps.Add(new CheepViewModel(author, message, timestampString));
                }
                return cheeps;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            List<CheepViewModel> emptyList = new List<CheepViewModel>();
            return emptyList;
        }
    }


    public static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }
    public string AddCheep(string username, string email, string pw_hash, int message_id, int author_id, string text, int pub_date)
    {
        return "Not implemented";
    }
}