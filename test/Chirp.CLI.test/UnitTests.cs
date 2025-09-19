using Chirp.CLI;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SimpleDB;


namespace test;

public class UnitTests
{
    [Fact]
    public void reads_correctly()
    {
        var csv = """
                  Author,Message,Timestamp
                  ropf,"Hello, BDSA students!",1690891760
                  adho,"Welcome to the course!",1690978778
                  adho,"I hope you had a good summer.",1690979858
                  ropf,"Cheeping cheeps on Chirp :)",1690981487
                  """;

     
        var baseDir = AppContext.BaseDirectory;

        var dataDir = Path.Combine(baseDir, "data");
        Directory.CreateDirectory(dataDir);

        var expectedPath = Path.Combine(dataDir, "chirp_cli_db.csv");
        File.WriteAllText(expectedPath, csv);

        // Now Instance can find its default file
        var cheeps = CSVDatabase<Cheep>.Instance.Read().ToList();
        
        Assert.Equal(4, cheeps.Count);

        Assert.Equal("ropf", cheeps[0].Author);
        Assert.Equal("Hello, BDSA students!", cheeps[0].Message);
        Assert.Equal(1690891760, cheeps[0].Timestamp);

        Assert.Equal("adho", cheeps[1].Author);
        Assert.Equal("Welcome to the course!", cheeps[1].Message);
        Assert.Equal(1690978778, cheeps[1].Timestamp);
        
        Assert.Equal("adho", cheeps[2].Author);
        Assert.Equal("I hope you had a good summer.", cheeps[2].Message);
        Assert.Equal(1690979858, cheeps[2].Timestamp);
        
        Assert.Equal("ropf", cheeps[3].Author);
        Assert.Equal("Cheeping cheeps on Chirp :)", cheeps[3].Message);
        Assert.Equal(1690981487, cheeps[3].Timestamp);
    }
}