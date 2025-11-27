using Xunit;

using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Security.Cryptography.X509Certificates;
using Moq;
namespace Chirp.Razor.Tests;
/*
 public class CheepServiceTests
{
    private readonly IDbFacade _db;
    private readonly CheepService _sut;
    public CheepServiceTests()
    {
        _db = new DBFacade();
        _sut = new CheepService(_db);
    }

    [Fact]
    public void testNameSpecificGetRequest()
    {
        var cheep = _sut.GetCheepsFromAuthor("Adrian")[0].Message;
        Assert.Equal("Hej, velkommen til kurset.", cheep);
    }

    [Fact]
    public void testGeneralGetRequest()
    {
        var cheeps = _sut.GetCheeps();
        Assert.Contains(cheeps, c => c.Message == "Hello, BDSA students!");
    }
    [Fact]
    public void TestAmountOfCheeps()
    {
        var cheepAmount = _sut.GetCheepCount();
        cheepAmount++;
        Assert.Equal(_sut.GetCheepCount() + 1, cheepAmount);
    }

    [Fact]
    public void TestUnixConverter()
    {
        UnixTimeStampToDateTimeString(1654867200);
        Assert.Equal("06/10/22 13.20.00", UnixTimeStampToDateTimeString(1654867200));
    }
    
    private static string UnixTimeStampToDateTimeString(double unixTimeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }
 }
 */

    
