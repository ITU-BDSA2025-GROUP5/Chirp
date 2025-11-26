using Chirp.Domain;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.VisualBasic;
using System;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;

    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int PageNumber { get; set; } = 1;
    public List<CheepDTO> Cheeps { get; set; } = new();
    public string? UserName { get; private set; }
    public User? CurrentUser { get; set; }
    public List<int> followedUsers { get; set; } = new();
    public PublicModel(ICheepService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        PageNumber = Math.Max(1, PageNumber);

        UserName = User.Identity?.Name;
        Cheeps = await _service.GetCheepsAsync(PageNumber);
        if (User.Identity?.IsAuthenticated == true && UserName != null)
        {
            var user = await _service.findUserByEmail(UserName);
            if (user != null)
            {
                CurrentUser = user;
                Console.WriteLine("The current user is " + CurrentUser.Name);
            }
            if (CurrentUser != null)
            {
                followedUsers = await _service.getFollowings(CurrentUser);
            }
        }
        return Page();
    }

    public async Task<IActionResult> OnPostNewMessageAsync(String Input)
    {
        if (User.Identity?.IsAuthenticated == false || User.Identity?.Name == null)
        {
            return Page();
        }
        var user = await _service.findUserByEmail(User.Identity.Name);
        if (user == null)
        {
            Console.WriteLine("No corresponding User found to login");
            return Page();
        }
        await _service.InsertCheepAsync(new CheepDTO
        {
            Text = Input,
            User = user,
            TimeStamp = DateTime.UtcNow
        });
        return RedirectToPage("Public");
    }

    public async Task<IActionResult> OnPostFollowAsync(int followeeId)
    {
        Console.WriteLine("This activates");
        var currentUserEmail = User.Identity?.Name;
        if (string.IsNullOrEmpty(currentUserEmail))
            return Unauthorized();

        var CurrentUser = await _service.findUserByEmail(currentUserEmail);
        if (CurrentUser == null) return Unauthorized();

        var ack = await _service.followUser(CurrentUser, followeeId);
        followedUsers = await _service.getFollowings(CurrentUser);
        Console.WriteLine(ack);
        return RedirectToPage("./Public");

    }

    public async Task<IActionResult> OnPostUnfollowAsync(int unfolloweeId)
    {
        Console.WriteLine("UnFollow activates");
        var currentUserEmail = User.Identity?.Name;
        if (string.IsNullOrEmpty(currentUserEmail))
        {
            Console.WriteLine("Sorry hombre pt. 1");
            return Unauthorized();
        }

        var CurrentUser = await _service.findUserByEmail(currentUserEmail);
        if (CurrentUser == null)
        {
            Console.WriteLine("Sorry hombre pt. 2");
            return Unauthorized();
        }

        var ack = await _service.UnfollowUser(CurrentUser, unfolloweeId);
        followedUsers = await _service.getFollowings(CurrentUser);
        Console.WriteLine(ack);

        return RedirectToPage("./Public");
    }
    
}