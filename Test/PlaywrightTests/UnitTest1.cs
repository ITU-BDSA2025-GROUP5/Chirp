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
        await Page.GotoAsync("http://localhost:5273/");
        
        await Expect(Page).ToHaveTitleAsync(new Regex("Log in - Chirp.Razor.web"));
    }
}