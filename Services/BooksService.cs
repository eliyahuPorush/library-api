using AutoMapper;
using Dal;
using Dal.Models;
using Domain.Dtos;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services;

public class BooksService : IBooksService
{
    private readonly BookStoreDbContext _db;
    private readonly IMapper _mapper;
    private readonly ILogger<BooksService> _logger;
    
    public BooksService(BookStoreDbContext db, IMapper mapper, ILogger<BooksService> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }

    public List<BookDto> GetAllBooks()
    {
        _logger.LogInformation("About to fetch all books from db");
        return _mapper.Map<List<BookDto>>(_db.Books.ToList());
    }
    public async Task<BookDto> GetBook(int id)
    {
        return _mapper.Map<BookDto>(await _db.Books.FindAsync(id));
    }

    public async Task<bool> AddBook(BookDto book)
    {
        var mappedBook = _mapper.Map<Book>(book);
        try
        {
            await _db.Books.AddAsync(mappedBook);
            await _db.SaveChangesAsync();
            
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        
    }

    public List<BookDto> GetAuthorBooks(int authorId)
    {
        var authorsBooks = _db.Books.Where(book => book.Author.Id == authorId);
        return _mapper.Map<List<BookDto>>(authorsBooks);
    }
}