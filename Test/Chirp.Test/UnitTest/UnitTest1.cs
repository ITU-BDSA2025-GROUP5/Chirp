using Xunit;


namespace Chirp.Razor.Tests;

public class CheepServiceTests
{
}

/*
{
private readonly CheepDbContext _ctx;
private readonly CheepRepo _cheepRepo;
private readonly CheepService _service;
private readonly UserRepository _userRepo;
public CheepServiceTests(SqliteInMemoryDbFixture fixture)
/*
{

   // old

   _db = new CheepDbContext();
   _sut = new CheepService(_db);

       _ctx = fixture.CreateContext();
       _cheepRepo = new CheepRepo(_ctx);
       _sut = new CheepService(_cheepRepo,_userRepo);

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
    
