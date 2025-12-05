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
using Chirp.Tests.Mock_Stub_Classes;

using Xunit;
[Collection("sqlite-db")]

public class FollowIntegrationTests
{
    

    private readonly ServiceProvider _provider;
    
    private readonly CheepDbContext _context;
    private readonly CheepRepository _cheepRepository;
    private readonly UserRepository _userRepository;
    private readonly CheepService _CheepService;
    private readonly UserService _UserService;
    private readonly UserManager <User> _UserManager;
    
    private readonly SqliteInMemoryDbFixture _fixture;
    
    public List<string> followedUsers { get; set; } = new();

    public FollowIntegrationTests(SqliteInMemoryDbFixture fixture)
    {
        _fixture = fixture;
        _context = fixture.CreateContext();
        _cheepRepository = new CheepRepository(_context);
        _userRepository = new UserRepository(_context);
        _UserService = new UserService(_userRepository,_UserManager);
        _CheepService = new CheepService(_cheepRepository, _UserService);
        
    }
    private async Task<(User follower, User followee)> CreateUsersAsync()
    {
            var follower = HelperClasses.createRandomUser();
            var followee = HelperClasses.createRandomUser();

            _context.Users.Add(follower);
            _context.Users.Add(followee);
            await _context.SaveChangesAsync();

            await _context.Entry(follower).ReloadAsync();
            await _context.Entry(followee).ReloadAsync();

            return (follower, followee);
    }


    [Fact]
        public async Task FollowAUser()
        {
            _fixture.ResetDatabase();
            
            var (follower, followee) = await CreateUsersAsync();

            var result = await _CheepService.followUser(follower, followee.Id);

            result.Should().NotBeNull();
        }
    
    [Fact]
        public async Task FollowAUserAndGetItOnFollowlist()
        {
            _fixture.ResetDatabase();

            var (follower, followee) = await CreateUsersAsync();

            var result = await _CheepService.followUser(follower, followee.Id);
            result.Should().NotBeNull();

            var followedUsers = await _CheepService.getFollowings(follower);

            followedUsers.Should().Contain(followee.Id);
        }

     [Fact]
        public async Task UnFollowAUser()
        {
            _fixture.ResetDatabase();

            var (follower, followee) = await CreateUsersAsync();

            var followResult = await _CheepService.followUser(follower, followee.Id);
            followResult.Should().NotBeNull();

            var unfollowResult = await _CheepService.UnfollowUser(follower, followee.Id);

            unfollowResult.Should().NotBeNull();
        }

    [Fact]
        public async Task UnFollowAUserAndRemoveItFromFollowlist()
        {
            _fixture.ResetDatabase();
            var (follower, followee) = await CreateUsersAsync();
            
            var followResult = await _CheepService.followUser(follower, followee.Id);
            followResult.Should().NotBeNull();

            var followedUsersBefore = await _CheepService.getFollowings(follower);
            followedUsersBefore.Should().Contain(followee.Id);

            var unfollowResult = await _CheepService.UnfollowUser(follower, followee.Id);
            unfollowResult.Should().NotBeNull();

            var followedUsersAfter = await _CheepService.getFollowings(follower);
            followedUsersAfter.Should().NotContain(followee.Id);
        }

}