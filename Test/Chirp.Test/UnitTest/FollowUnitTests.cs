using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Chirp.Domain;
using Chirp.Infrastructure;
using Chirp.Tests.Infrastructure;
using Chirp.Tests.Tools_to_Test;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Chirp.Tests.Mock_Stub_Classes;

using Xunit;

public class FollowUnitTests
{
    private readonly ServiceProvider _provider;
    private readonly UserManager<User> _userManager;
    private InputFuzzers  _inputFuzzers;

    private readonly ICheepService _service;

    public List<string> followedUsers { get; set; } = new();

    public FollowUnitTests()
    {
    _service = new CheepServiceStub();
    }


    [Fact]
    public async Task FollowAUser()
    {
        var user = new User { UserName = "validname", Email = "Very_Much_an_email@itu.dk" , Cheeps = new List<Cheep>() };
        var user2 = new User { UserName = "validname2", Email = "Very_Much_an_email2@itu.dk" , Cheeps = new List<Cheep>() };

        
        var result = await _service.followUser(user, user2.Id);

        Assert.True(result is not null);
    }

    [Fact]
    public async Task FollowAUserAndGetItOnMyTimeline()
    {
        var user = new User { UserName = "validname", Email = "Very_Much_an_email@itu.dk" , Cheeps = new List<Cheep>() };
        var user2 = new User { UserName = "validname2", Email = "Very_Much_an_email2@itu.dk" , Cheeps = new List<Cheep>() };

        
        var result = await _service.followUser(user, user2.Id);

        followedUsers = await _service.getFollowings(user);

        Assert.True(followedUsers.Contains(user2.Id));
    }
}