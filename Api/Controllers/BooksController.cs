using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService)
    {
        _booksService = booksService;
    }
    
    [HttpGet, Route("{id}")]
    public async Task<IActionResult> GetBook(int id)
    {
        return Ok(await _booksService.GetBook(id));
    }

    [HttpGet]
    public IActionResult GetAuthorBooks([FromQuery] int authorId)
    {
        if (authorId <= 0)
        {
            return Ok(_booksService.GetAllBooks());
        }
        return Ok(_booksService.GetAuthorBooks(authorId));
    }
    
    [HttpPost]
    public async Task<IActionResult>  AddBook(BookDto book)
    {
        return Ok(await _booksService.AddBook(book));
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook(string id)
    {
        await _booksService.DeleteBook(id);
        return NoContent();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateBook(int id, BookDto book)
    {
        await _booksService.UpdateBook(id, book);
        return NoContent();
    }
    
}