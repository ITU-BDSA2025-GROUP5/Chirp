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

    public UserTimelineModel(ICheepService service)
    {
        _service = service;
    }

    public async Task<ActionResult> OnGet(string author)
    {
        var user = await _service.findAuthorByEmail(author);
        if (user == null)
        {
            Console.WriteLine("No corresponding user found");
            return Page();
        }
        Cheeps = await _service.getCheepsFromUser(user, 1);
        return Page();
    }
    
}

