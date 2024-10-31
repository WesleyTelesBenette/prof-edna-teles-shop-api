using Microsoft.AspNetCore.Mvc;
using prof_edna_teles_shop_api.DTOs;
using prof_edna_teles_shop_api.Services.Interfaces;

namespace prof_edna_teles_shop_api.Controllers;

[ApiController]
[Route("[controller]")]
public class PageController : ControllerBase
{
    private readonly IPageService _pageService;
    public PageController(IPageService pageService)
    {
        _pageService = pageService;
    }

    [HttpGet("home")]
    public async Task<IActionResult> GetHomePageContent()
    {
        PageResponseDTO? pageContent = await _pageService.GetHomePageContent();

        return (pageContent != null)
            ? Ok(pageContent)
            : NotFound();
    }
}
