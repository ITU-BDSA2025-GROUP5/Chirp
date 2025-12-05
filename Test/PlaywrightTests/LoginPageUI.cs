using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;


[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class LoginPageUI : PageTest
{
    /*
    [Test]
    public async Task LoginPageLoadsAndHasCorrectTitle()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        
        await Expect(Page).ToHaveTitleAsync(new Regex("Log in - Chirp.Razor.Web"));
    }

    [Test]
    public async Task LoginPageLoadsAndHasCorrectContent()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        
        await Expect(Page.GetByText("Use a local account to log in.")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Use another service to log in.")).ToBeVisibleAsync();
    }

    [Test]
    public async Task LoginPageHasLoginButtonAndItsClickable()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        
        // the button
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Log in" })).ToBeVisibleAsync();
        //its clickable
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
    }
    
    [Test]
    public async Task LoginPageHasGithubLoginButtonAndItsClickable()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        
        // the button
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "GitHub" })).ToBeVisibleAsync();
        //its clickable
        await Page.GetByRole(AriaRole.Button, new() { Name = "GitHub" }).ClickAsync();
    }

    [Test]
    public async Task LoginPageHasInputFields()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        
        // email and password fields/labels
        await Expect(Page.GetByLabel("Email")).ToBeVisibleAsync();
        await Expect(Page.GetByLabel("Password")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task LoginPageHasRememberMeCheckboxAndItsClickable()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        
        // the checkfield / box
        await Expect(Page.GetByRole(AriaRole.Checkbox, new() { Name = "Remember me?" })).ToBeVisibleAsync();
        // its clickable
        await Page.GetByRole(AriaRole.Checkbox, new() { Name = "Remember me?" }).CheckAsync();
    }
    
    // I have splitted up tests for the three links, because to test if clickable we need it in seperate tests, because it directs to the first link.
    [Test]
    public async Task LoginPageHasLinks()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        
        // the 3 links under login button
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Forgot your password?" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Register as a new user" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Resend email confirmation" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task ForgotPasswordLinkIsClickable()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Forgot your password?" }).ClickAsync();
    }
    [Test]
    public async Task RegisterAsNewUserLinkIsClickable()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register as a new user" }).ClickAsync();
    }
    [Test]
    public async Task ResendEmailLinkIsClickable()
    {
        await Page.GotoAsync("http://localhost:7103/Identity/Account/Login");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Resend email confirmation" }).ClickAsync();
    }
    */
}