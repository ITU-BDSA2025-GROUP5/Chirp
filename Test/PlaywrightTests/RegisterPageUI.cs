using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class RegisterPageUI : PageTest
{
/*
    [Test]
    public async Task RegisterPageLoadsAndHasCorrectTitleShows()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Register");
        
        await Expect(Page).ToHaveTitleAsync(new Regex("Register - Chirp.Razor.Web"));
        
    }

    [Test]
    public async Task LoginPageLoadsAndHasCorrectContent()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Register");
        
        await Expect(Page.GetByText("Create a new account")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Use another service to register.")).ToBeVisibleAsync();
    }

    [Test]
    public async Task RegisterPageHasEmailFields()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Register");
        await Expect(Page.GetByLabel("Email")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task RegisterPageHasPasswordFields()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Register");
        await Expect(Page.GetByLabel("Password", new() { Exact = true })).ToBeVisibleAsync();
        // exact true is used because it kept getting errors without because of the field confirm password contains the word password aswell
    }
    
    [Test]
    public async Task RegisterPageHasConfirmPasswordFields()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Register");
        await Expect(Page.GetByLabel("Confirm Password")).ToBeVisibleAsync();
    }

    [Test]
    public async Task RegisterPageHasRegisterButtonAndItsClickable()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Register");
        // Its showing
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Register" })).ToBeVisibleAsync();
        // Its clickable
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
    }

    [Test]
    public async Task RegisterHasGithubButtonAndItsClickable()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Register");
        // Its Showing
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Github" })).ToBeVisibleAsync();
        // Its Clickable
        await Page.GetByRole(AriaRole.Button, new() { Name = "Github" }).ClickAsync();
    }
    */
}
