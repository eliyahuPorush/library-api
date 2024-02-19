using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public IActionResult Search(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Query parameter is required");
        }
    
        return Ok(_searchService.Search(query));
    }
}