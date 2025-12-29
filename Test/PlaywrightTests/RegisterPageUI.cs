using System.Text.RegularExpressions;
using Chirp.PlaywrightTests;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]

public class RegisterPageUI : PlaywrightTestBase
{

    [SetUp]
    public async Task GoToRegisterPage()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register account" }).ClickAsync(); 
        
    }
    
    // all following test will be for when users are not logged in
    
    [Test]
    public async Task RegisterPageLoadsAndHasCorrectTitleShows()
    {
        
        await Expect(Page).ToHaveTitleAsync(new Regex("Register"));
        
    }

    [Test]
    public async Task LoginPageLoadsAndHasCorrectContent()
    {
        await Expect(Page.GetByText("Create a new account")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Use another service to register.")).ToBeVisibleAsync();
    }

    [Test]
    public async Task RegisterPageHasEmailFields()
    {
        await Expect(Page.GetByLabel("Email")).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task RegisterPageHasPasswordField()
    {
        
        await Expect(Page.GetByLabel("Password", new() { Exact = true })).ToBeVisibleAsync();
        // exact true is used because it kept getting errors without because of the field confirm password contains the word password aswell
    }
    
    [Test]
    public async Task RegisterPageHasConfirmPasswordField()
    {
        await Expect(Page.GetByLabel("Confirm Password")).ToBeVisibleAsync();
    }

    [Test]
    public async Task RegisterPageHasRegisterButton()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Register" })).ToBeVisibleAsync();
    }

    [Test]
    public async Task RegisterHasGithubButton()
    {
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Github" })).ToBeVisibleAsync();
    }
    
}
