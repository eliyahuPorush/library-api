using AutoMapper;
using Dal;
using Dal.Models;
using Domain.Dtos;
using Microsoft.EntityFrameworkCore;
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
        return _mapper.Map<List<BookDto>>(_db.Books.Include(book => book.Author).AsNoTracking().ToList());
    }

    public async Task<BookDto> GetBook(int id)
    {
        _logger.LogInformation($"[BooksService][GetBook] About to fetch book with book id {id} from db");
        var book = await _db.Books.Include(b => b.Author)
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();
        return _mapper.Map<BookDto>(book);
    }

    public async Task<int> AddBook(BookDto book)
    {
        var mappedBook = _mapper.Map<Book>(book);
        try
        {
            _logger.LogInformation($"[BooksService][AddBokk] About to add book with book id {mappedBook.Id} to db");
            await _db.Books.AddAsync(mappedBook);
            var newBook = await _db.Books.AddAsync(mappedBook);
            await _db.SaveChangesAsync();
            _logger.LogInformation($"[BooksService][AddBook] book with id: {newBook.Entity.Id} added sucessfully");
            return newBook.Entity.Id;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteBook(int id)
    {
        try
        {
            _logger.LogInformation($"[BooksService] [DeleteBook] about to delete book with id: {id}");
            _db.Books.Remove(new Book{Id = id});
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
                _logger.LogError($"[BooksService] [DeleteBook] book id: {id} is not exist on db and cannot be delete");
                throw new Exception($"book id: {id} is not exist on db and cannot be delete, exception message: {ex.Message}");
        }
    }

    public async Task UpdateBook(BookDto book)
    {
        var mappedBook = _mapper.Map<Book>(book);
        var bookToUpdate = await _db.Books.FindAsync(book.Id);
        if (bookToUpdate is null) throw new Exception($"can't find book with id: {book.Id} to update");
        _db.Entry(bookToUpdate).CurrentValues.SetValues(mappedBook);
        await _db.SaveChangesAsync();
    }

    public List<BookDto> GetAuthorBooks(int authorId)
    {
            
        _logger.LogInformation($"[BooksService][GetAuthorBooks] About to fetch books for author id {authorId} from db");
        var authorsBooks = _db.Books.Where(book => book.Author.Id == authorId);         
        _logger.LogInformation($"[BooksService][GetAuthorBooks] books fetched sucessfully for author id {authorId}");
        return _mapper.Map<List<BookDto>>(authorsBooks);
    }
}