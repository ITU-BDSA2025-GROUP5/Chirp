public class End2End
{
    [Fact]
    public void TestReadCheep()
    {
        // Arrange
        Assert.True(File.Exists("data.csv"));
        // Act
        string output = "";
        using (var process = new Process())
        {
            process.StartInfo.FileName = "C:/Users/cfred/.dotnet";
            process.StartInfo.Arguments = "./src/Chirp.CLI.Client/bin/Debug/net8.0/chirp.dll read";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.WorkingDirectory = "C:/Users/cfred/OneDrive/ITU/3. Semester/Analysis, Design and Software Architecture/Git/Chirp/Chirp/src/Chirp.CLI";
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            // Synchronously read the standard output of the spawned process.
            StreamReader reader = process.StandardOutput;
            output = reader.ReadToEnd();
            process.WaitForExit();
        }
        string fstCheep = output.Split("\n")[0];
        // Assert
        Assert.StartsWith("ropf", fstCheep);
        Assert.EndsWith("Hello, World!", fstCheep);
    }
}