using Microsoft.Data.Sqlite;
namespace Chirp.Razor.Data;

public sealed class DBFacade : IDbFacade
{
    public int GetCheepCount()
    {
        var sqlDBFilePath = "/tmp/init.db";
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
}