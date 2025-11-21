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
        if (User.Identity?.IsAuthenticated == true || User.Identity?.Name != null)
        {
            Console.WriteLine(User.Identity?.Name);
            var user = await _service.findUserByEmail(User.Identity?.Name);
            if (user != null)
            {
                followedUsers = await _service.getFollowings(user);
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

    public async Task<IActionResult> OnPostFollowAsync(int followeeID)
    {
        var user = await _service.findUserByEmail(User.Identity?.Name);
        var ack = await _service.followUser(user, followeeID);
        
        followedUsers = await _service.getFollowings(user);

        return RedirectToPage();
        
    }
    
    public async Task<IActionResult> OnPostUnFollowAsync(int followeeId)
    {
        var currentUserEmail = User.Identity?.Name;
        if (string.IsNullOrEmpty(currentUserEmail))
            return Unauthorized();

        var follower = await _service.findUserByEmail(currentUserEmail);
        if (follower == null)
            return Unauthorized();

        await _service.UnfollowUserAsync(follower, followeeId);

        return RedirectToPage();
    }
    
}