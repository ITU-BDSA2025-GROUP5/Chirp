using Chirp.Domain;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepDTO> Cheeps { get; set; } = new();

    public List<CheepDTO> CheepsFromFollowings { get; set; } = new();
    public string? UserName { get; private set; }

    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int PageNumber { get; set; } = 1;

    public User? CurrentUser { get; set; }

    public List<int> followedUsers { get; set; } = new();

    public UserTimelineModel(ICheepService service)
    {
        _service = service;
    }

    public async Task<ActionResult> OnGet(string author)
    {
        PageNumber = Math.Max(1, PageNumber);

        UserName = User.Identity?.Name;
        if (User.Identity?.IsAuthenticated == true && UserName != null)
        {
            var user = await _service.findUserByEmail(UserName);
            if (user != null)
            {
                CurrentUser = user;
                Console.WriteLine("The current user is " + CurrentUser.Name);
                Cheeps = await _service.getCheepsFromUser(CurrentUser, PageNumber);
            }
            if (CurrentUser != null)
            {
                followedUsers = await _service.getFollowings(CurrentUser);
            }
        }

        if (followedUsers != null)
        {
            Console.WriteLine("Step 1");
            foreach (var userId in followedUsers)
            {
                Console.WriteLine("Step 2");
                var tempCheeps = await _service.GetCheepsFromUserId(userId);
                foreach (var cheep in tempCheeps)
                {
                    Console.WriteLine("Step 3");
                    CheepsFromFollowings.Add(cheep);
                }
            }
        }
        if (CheepsFromFollowings.Count > 0)
        {
            Console.WriteLine("Ummmmmmmm");
        }
        return Page();
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

        return RedirectToPage();
    }
    
}

