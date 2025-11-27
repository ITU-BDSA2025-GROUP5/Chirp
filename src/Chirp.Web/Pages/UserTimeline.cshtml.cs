using Chirp.Domain;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service ;
    public List<CheepDTO> Cheeps { get; set; } = new();

    public List<CheepDTO> CheepsFromFollowings { get; set; } = new();
    public string? UserName { get; private set; }

    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int PageNumber { get; set; } = 1;

    public User? CurrentUser { get; set; }

    public List<string> followedUsers { get; set; } = new();

    public UserTimelineModel(ICheepService service)
    {
        _service = service;
    } 

    public async Task<IActionResult> OnGet(string author)
{
    UserName = User.Identity?.Name;
    PageNumber = Math.Max(1, PageNumber);

    var timelineUser = await _service.findUserByEmail(author);
    if (timelineUser != null)
    {
        Cheeps = await _service.getCheepsFromUser(timelineUser, PageNumber);
    }

    if (User.Identity?.IsAuthenticated == true && UserName != null)
    {
        var user = await _service.FindTimelineByUserNameAsync(author);
        if (user == null)
        {
            followedUsers = await _service.getFollowings(CurrentUser);

            if (followedUsers != null)
            {
                foreach (var userId in followedUsers)
                {
                    var tempCheeps = await _service.GetCheepsFromUserId(userId);
                    CheepsFromFollowings.AddRange(tempCheeps);
                }
            }
        }
        Cheeps = await _service.getCheepsFromUser(user, 1); 
        return Page();
    }
    
}


