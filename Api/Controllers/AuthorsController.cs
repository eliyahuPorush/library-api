using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly IAuthorsService _authorsService;

    public AuthorsController(IAuthorsService authorsService)
    {
        _authorsService = authorsService;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        return Ok(_authorsService.GetAuthors());
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor(AuthorDto author)
    {
        var authorId = await _authorsService.AddAuthorAsync(author);
        return CreatedAtAction(nameof(GetAuthors), new {Id = authorId});
    }

    [HttpDelete("{authorId}")]
    public async Task<IActionResult> DeleteAuthor([FromRoute] int authorId)
    {
        await _authorsService.DeleteAuthorAsync(authorId);        
        return NoContent();
    }
    
    [HttpPatch]
    public async Task<IActionResult> UpdateAuthor(int id, AuthorDto author)
    {
        await _authorsService.UpdateAuthor(id, author);
        return NoContent();
    }
}