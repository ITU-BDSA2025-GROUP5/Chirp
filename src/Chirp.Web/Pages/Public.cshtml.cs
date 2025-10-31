using Chirp.Domain;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using System;

namespace Chirp.Razor.Pages;
[Authorize]
public class PublicModel : PageModel
{
    private readonly ICheepService _service;

    [BindProperty(SupportsGet = true, Name = "pagenumber")]
    public int PageNumber { get; set; } = 1;
    public List<CheepDTO> Cheeps { get; set; } = new();
    public string? UserName { get; private set; }
    public PublicModel(ICheepService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        PageNumber = Math.Max(1, PageNumber);
        UserName = User.Identity?.Name;    
        Cheeps = await _service.GetCheepsAsync(PageNumber);
        return Page();
    }

    public async Task<IActionResult> OnPostNewMessageAsync(String Input)
    {
        
        await _service.InsertCheepAsync(new CheepDTO
        {
            Text = Input,
            User = null,
            TimeStamp = DateTime.UtcNow
        });
        return RedirectToPage("Public");
    }
}