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
[Collection("sqlite-db")]

public class FollowIntegrationTests
{
    private readonly SqliteInMemoryDbFixture _fixture;

    private readonly ServiceProvider _provider;

    private CheepDbContext CreateContext() => _fixture.CreateContext();
    private readonly UserManager<User> _userManager;
    private InputFuzzers  _inputFuzzers;

    private ICheepService CreateCheepService(CheepDbContext context)
    {
    return new CheepServiceStub(); 
    }

    private readonly ICheepService _service;

    public List<string> followedUsers { get; set; } = new();

    public FollowIntegrationTests(SqliteInMemoryDbFixture fixture)
    {
    _fixture = fixture;
    _service = new CheepServiceStub();
    }
    private async Task<(User follower, User followee)> CreateUsersAsync(CheepDbContext ctx)
        {
            var follower = new User
            {
                UserName = "name",
                Email = "Very_Much_an_email@itu.dk",
                Cheeps = new List<Cheep>()
            };

            var followee = new User
            {
                UserName = "name2",
                Email = "Very_Much_an_email2@itu.dk",
                Cheeps = new List<Cheep>()
            };

            ctx.Users.Add(follower);
            ctx.Users.Add(followee);
            await ctx.SaveChangesAsync();

            await ctx.Entry(follower).ReloadAsync();
            await ctx.Entry(followee).ReloadAsync();

            return (follower, followee);
    }


    [Fact]
        public async Task FollowAUser()
        {
            _fixture.ResetDatabase();
            using var ctx = CreateContext();
            var service = CreateCheepService(ctx);

            var (follower, followee) = await CreateUsersAsync(ctx);

            var result = await service.followUser(follower, followee.Id);

            result.Should().NotBeNull();
        }
    
    [Fact]
        public async Task FollowAUserAndGetItOnFollowlist()
        {
            _fixture.ResetDatabase();
            using var ctx = CreateContext();
            var service = CreateCheepService(ctx);

            var (follower, followee) = await CreateUsersAsync(ctx);

            var result = await service.followUser(follower, followee.Id);
            result.Should().NotBeNull();

            var followedUsers = await service.getFollowings(follower);

            followedUsers.Should().Contain(followee.Id);
        }

     [Fact]
        public async Task UnFollowAUser()
        {
            _fixture.ResetDatabase();
            using var ctx = CreateContext();
            var service = CreateCheepService(ctx);

            var (follower, followee) = await CreateUsersAsync(ctx);

            var followResult = await service.followUser(follower, followee.Id);
            followResult.Should().NotBeNull();

            var unfollowResult = await service.UnfollowUser(follower, followee.Id);

            unfollowResult.Should().NotBeNull();
        }

    [Fact]
        public async Task UnFollowAUserAndRemoveItFromFollowlist()
        {
            _fixture.ResetDatabase();
            using var ctx = CreateContext();
            var service = CreateCheepService(ctx);

            var (follower, followee) = await CreateUsersAsync(ctx);

            var followResult = await service.followUser(follower, followee.Id);
            followResult.Should().NotBeNull();

            var followedUsersBefore = await service.getFollowings(follower);
            followedUsersBefore.Should().Contain(followee.Id);

            var unfollowResult = await service.UnfollowUser(follower, followee.Id);
            unfollowResult.Should().NotBeNull();

            var followedUsersAfter = await service.getFollowings(follower);
            followedUsersAfter.Should().NotContain(followee.Id);
        }

}