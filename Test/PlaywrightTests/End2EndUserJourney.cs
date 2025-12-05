using System.Text.RegularExpressions;
using Chirp.PlaywrightTests;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class End2EndUserJourney : PlaywrightTestBase
{
    [Test, Order(1)]
    public async Task RegisterLoginAndPostACheep()
    {
        await Page.GotoAsync(BaseUrl);

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        
        await Page.GetByLabel("Email").FillAsync("TestMail@Chirp.com");
        await Page.GetByLabel("Password").FillAsync("Password123.");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        
        await Page.WaitForURLAsync(BaseUrl);

        await Page.WaitForSelectorAsync("input[name='Input']");

        await Page.Locator("input[name='Input']").FillAsync("This is a test cheep!"); // got this from copilot
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        
        await Expect(Page.GetByText("TestMail@Chirp.com This is a test cheep!")).ToBeVisibleAsync();
        
    }

    [Test, Order(2)]
    public async Task LoginAndGoToMyTimeline()
    {
        // This is the process of a user logging in with test account, and going to its own timeline
        await Page.GotoAsync(BaseUrl);

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
        await Page.GotoAsync(BaseUrl);

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        
        await Page.GetByLabel("Email").FillAsync("TestMail@Chirp.com");
        await Page.GetByLabel("Password").FillAsync("Password123.");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "Logout [TestMail@Chirp.com]" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Click here to Logout" }).ClickAsync();

        await Expect(Page.GetByText("Use a local account to log in.")).ToBeVisibleAsync();
    }
}