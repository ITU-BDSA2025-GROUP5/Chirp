using System.Text.RegularExpressions;
using Chirp.PlaywrightTests;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;


[Parallelizable(ParallelScope.None)]
[TestFixture]
public class LoginPageUI : PlaywrightTestBase
{
    
    [SetUp]
    public async Task GoToLoginPage()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync(); 
        
    }
    
    // all following test will be for when users are not logged in
    
    [Test]
    public async Task LoginPageLoadsAndHasCorrectTitle()
    {
        
        await Expect(Page).ToHaveTitleAsync(new Regex("Log in"));
    }

    [Test]
    public async Task LoginPageLoadsAndHasCorrectContent()
    {
        await Expect(Page.GetByText("Use a local account to log in.")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Use another service to log in.")).ToBeVisibleAsync();
    }

    [Test]
    public async Task LoginPageHasLoginButton()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Log in" })).ToBeVisibleAsync();
    
    }
    
    [Test]
    public async Task LoginPageHasGithubLoginButton()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "GitHub" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task LoginPageHasInputFields()
    {
        // email and password fields/labels
        await Expect(Page.GetByLabel("Email")).ToBeVisibleAsync();
        await Expect(Page.GetByLabel("Password")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task LoginPageHasRememberMeCheckbox()
    {
        // the checkbox
        await Expect(Page.GetByRole(AriaRole.Checkbox, new() { Name = "Remember me?" })).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task LoginPageHasLinks()
    {
        // the 3 links under login button
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Forgot your password?" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Register as a new user" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Resend email confirmation" })).ToBeVisibleAsync();
    }
    
}