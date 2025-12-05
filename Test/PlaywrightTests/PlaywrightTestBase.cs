using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Chirp.PlaywrightTests;

[TestFixture]
public class PlaywrightTestBase : PageTest
{
    private CustomWebApplicationFactory _factory = null!;
    public string BaseUrl { get; private set; } = null!;

    [SetUp]
    public async Task GlobalSetup()
    {
        // 1. Create factory
        _factory = new CustomWebApplicationFactory();

        // 2. Trigger server start
        _factory.CreateClient();

        // 3. Get URL
        BaseUrl = _factory.ClientOptions.BaseAddress.ToString();

        // 4. INCREASE TIMEOUT
        // Give the app 60 seconds to boot up and migrate DB before failing
        Page.SetDefaultTimeout(60000);

        // 5. Navigate
        // We use the extended timeout here specifically for the first load
        await Page.GotoAsync(BaseUrl, new() { Timeout = 60000 });
    }

    [TearDown]
    public async Task GlobalTeardown()
    {
        await _factory.DisposeAsync();
    }
}