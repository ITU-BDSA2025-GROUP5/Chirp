using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task HomePageLoadsAndHasCorrectTitle()
    {
        await Page.GotoAsync("http://localhost:7103/");
        
        await Expect(Page).ToHaveTitleAsync(new Regex("BBL CHIRP!"));
    }

    [Test]
    public async Task HomePageLoadsAndHasCorrectContent()
    {
        
        await Page.GotoAsync("http://localhost:7103/");
        
        
    }
    
    
}