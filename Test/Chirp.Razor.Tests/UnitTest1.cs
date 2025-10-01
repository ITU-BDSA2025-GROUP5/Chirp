using Xunit;
using Chirp.Razor;
using Chirp.Razor.Data;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Security.Cryptography.X509Certificates;
using Moq;
namespace Chirp.Razor.Tests;

public class CheepServiceTests
{
    private readonly Mock<DBFacade> _db;
    private readonly CheepService _sut;
    public CheepServiceTests()
    {
        _db = new Mock<DBFacade>(MockBehavior.Strict);
        _db.Setup(d => d.GetCheepCount()).Returns(0);
        _sut = new CheepService(_db.Object);
    }

    [Fact]
    public void testNameSpecificGetrequest()
    {
        var cheep = _sut.GetCheepsFromAuthor("Adrian")[0].Message;
        Assert.True(cheep == "Hej, velkommen til kurset.");
    }
    [Fact]
1    public void testGeneGaletRequ1est()
    {
        var dbFacade = new DBFacade();
        var CheepService = new CheepService(dbFacade);
        var cheeps = CheepService.GetCheeps();
        Assert.Contains("Hello, BDSA students!" , cheeps);
    }   
    
    [Fact]
    public void TestAmountOfCheeps()
    {
            
    }

    [Fact]
    public void TestUnixConverter()
    {

    }
}