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

    [BindProperty]
    public InputModel Input { get; set; } = new();
    public class InputModel
    {
        public string Author { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
    public List<MessageDTO> Cheeps { get; set; } = new();

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
    
    public async Task<IActionResult> OnPostNewMessageAsync()
    {
        var author = string.IsNullOrWhiteSpace(Input.Author) ? "Anonymous" : Input.Author.Trim();

        var newUser = new User { Name = Input.Author, Email = $"{author}-{Guid.NewGuid():N}@local", Cheeps = new List<Cheep>() };

        await _service.InsertCheepAsync(new MessageDTO {
            Text = Input.Text,
            User = newUser,
            TimeStamp = DateTime.UtcNow
        });

        return RedirectToPage("Public");
    }
}
