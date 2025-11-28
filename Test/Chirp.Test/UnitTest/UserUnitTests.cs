using Chirp.Domain;
using Chirp.Infrastructure;
//using Chirp.Razor.Tests.Infrastructure;
using Chirp.Tests.Infrastructure;
using Chirp.Tests.Tools_to_Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chirp.Tests.UnitTest;

[Collection("sqlite-db")]

public class IdentityUserTests
{
    private readonly ServiceProvider _provider;
    private readonly UserManager<User> _userManager;
    private InputFuzzers  _inputFuzzers;
    public IdentityUserTests(SqliteInMemoryDbFixture fixture)
    {
        // Build a service provider with EF Core + Identity
        var services = new ServiceCollection();
        
        //unit test (it doesnt test on actual db)
        services.AddDbContext<CheepDbContext>(options =>
            options.UseInMemoryDatabase("IdentityTestDb"));

        services.AddDefaultIdentity<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<CheepDbContext>();

        _provider = services.BuildServiceProvider();
        _userManager = _provider.GetRequiredService<UserManager<User>>();
    }

    [Fact]
    public async Task CreateUserWithIncorrectUserName_fails()
    {
        var user = new User { UserName = "validname", Email = "not-an-email" , Cheeps = new List<Cheep>() };
        var result = await _userManager.CreateAsync(user, password: "pA88w0rd.");

        Assert.False(result.Succeeded);
        Assert.Contains(result.Errors, e => e.Code == "InvalidEmail");

    }

    [Fact]
    public async Task CreateUserWithCorrectPassword()
    {
        var user = new User { UserName = "validname", Email = "Very_Much_an_email@itu.dk" , Cheeps = new List<Cheep>() };
        var result = await _userManager.CreateAsync(user, password: "pA88w0rd.");

        Assert.True(result.Succeeded);
    }
    
}