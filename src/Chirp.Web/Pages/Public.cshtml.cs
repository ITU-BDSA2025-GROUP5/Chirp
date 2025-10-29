using Chirp.Domain;
using Chirp.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public int PageNumber { get; private set; }
    private const int pageSize = 32;
    public List<CheepDTO> Cheeps { get; private set; } = new();

    public PublicModel(ICheepService service)
    {
        _service = service;
    }

    public async Task<ActionResult> OnGet(int PageNumber)
    {
        Cheeps = await _service.GetCheepsAsync(PageNumber);
        return Page();
    }
}
