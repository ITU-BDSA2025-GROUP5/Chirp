using System.Text.RegularExpressions;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class HomePageUI : PageTest
{
    // all following test will be for when users are not logged in
    [Test]
    public async Task HomePageLoadsAndHasCorrectTitle()
    {
        await Page.GotoAsync("http://localhost:7103/");
        
        await Expect(Page).ToHaveTitleAsync(new Regex("BBL CHIRP!"));
    }

    [Test]
    public async Task HomePageShowPublicTimeline()
    {
        
        await Page.GotoAsync("http://localhost:7103/");
        
        await Expect(Page.GetByText("Public Timeline")).ToBeVisibleAsync();
    }

    [Test]
    public async Task HomePageNavBarHasCorrectButtons ()
    {
        await Page.GotoAsync("http://localhost:7103/");
        
        await Expect(Page.GetByText("Public ChirpLine")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Register account")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Login")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task HomePageShowsChirpTitle ()
    {
        await Page.GotoAsync("http://localhost:7103/");
        
        await Expect(Page.GetByText("Chirp!")).ToBeVisibleAsync();
    }

    
}