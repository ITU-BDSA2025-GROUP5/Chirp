using Chirp.Domain;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;

    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int PageNumber { get; set; } = 1;
    public List<CheepDTO> Cheeps { get; set; } = new();

    public PublicModel(ICheepService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        PageNumber = Math.Max(1, PageNumber);
        Cheeps = await _service.GetCheepsAsync(PageNumber);
        return Page();
    }
    
    public async Task<IActionResult> OnPostNewMessageAsync(String Input)
    {
        var newUser = new User { Name = "TEST", Email = "testEmail@123", Cheeps = new List<Cheep>() };

        await _service.InsertCheepAsync(new CheepDTO {
            Text = Input,
            User = newUser,
            TimeStamp = DateTime.UtcNow
        });
        return RedirectToPage("Public");
    }
}