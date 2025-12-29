using System.Text.RegularExpressions;
using Chirp.PlaywrightTests;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;
[TestFixture]
public class End2EndUserJourney : PlaywrightTestBase
{
    [Test, Order(1)]
    public async Task UserCanGoToRegisterPageAndRegister()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register account" }).ClickAsync();
        
        await Page.GetByLabel("Email").FillAsync("TestMail@Chirp.com");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Password123.");
        await Page.GetByLabel("Confirm Password", new() { Exact = true }).FillAsync("Password123.");
        
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        
        await Expect(Page.GetByText("Register confirmation")).ToBeVisibleAsync();
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "Click here to confirm your account" }).ClickAsync();
    }

    [Test, Order(2)]
    public async Task UserCanNowGoAndLogin_PostACheep_SeeItOnOwnTimeline()
    {
        await Page.GotoAsync(BaseUrl);
        
        // login
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByLabel("Email").FillAsync("TestMail@Chirp.com");
        await Page.GetByLabel("Password").FillAsync("Password123.");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

        await Page.WaitForURLAsync(BaseUrl);

        // make test cheep and see it afterwards
        await Page.WaitForSelectorAsync("input[name='Input']");
        await Page.Locator("input[name='Input']").FillAsync("This is a test cheep!"); // got this from copilot
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        await Expect(Page.GetByText("TestMail@Chirp.com This is a test cheep!")).ToBeVisibleAsync();
        
        // go to own timeline and see previous cheep
        await Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" }).ClickAsync();
        await Expect(Page.GetByText("TestMail@Chirp.com's Timeline")).ToBeVisibleAsync();
        await Expect(Page.GetByText("TestMail@Chirp.com This is a test cheep!")).ToBeVisibleAsync();
        
    }
}