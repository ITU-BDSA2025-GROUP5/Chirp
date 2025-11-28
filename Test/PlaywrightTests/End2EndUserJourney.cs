using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class End2EndUserJourney : PageTest
{
    [Test, Order(1)]
    public async Task RegisterLoginAndPostACheep()
    {
        // this is the process of going to homepage, logging in with test account, and posting a cheep (and its visible).
        
        await Page.GotoAsync("http://localhost:7103/");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        
        await Page.GetByLabel("Email").FillAsync("TestMail@Chirp.com");
        await Page.GetByLabel("Password").FillAsync("Password123.");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        
        await Page.Locator("input[name='Input']").FillAsync("This is a test cheep!"); // got this from copilot
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        
        await Expect(Page.GetByText("TestMail@Chirp.com This is a test cheep!")).ToBeVisibleAsync();
        
    }

    [Test, Order(2)]
    public async Task LoginAndGoToMyTimeline()
    {
        // This is the process of a user logging in with test account, and going to its own timeline
        await Page.GotoAsync("http://localhost:7103/");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        
        await Page.GetByLabel("Email").FillAsync("TestMail@Chirp.com");
        await Page.GetByLabel("Password").FillAsync("Password123.");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" }).ClickAsync();
        
        await Expect(Page.GetByText("TestMail@Chirp.com's Timeline")).ToBeVisibleAsync();
    }
    
    [Test, Order(3)]
    public async Task LoginAndLogoutAgain()
    {
        // This is the process of a user logging in with test account and logging out again.
        await Page.GotoAsync("http://localhost:7103/");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        
        await Page.GetByLabel("Email").FillAsync("TestMail@Chirp.com");
        await Page.GetByLabel("Password").FillAsync("Password123.");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "Logout [TestMail@Chirp.com]" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Click here to Logout" }).ClickAsync();

        await Expect(Page.GetByText("Use a local account to log in.")).ToBeVisibleAsync();
    }
}