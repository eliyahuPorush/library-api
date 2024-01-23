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
    public async Task<IActionResult> GetBook([FromRoute] int id)
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
    
}