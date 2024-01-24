using Domain.Dtos;

namespace Services.Interfaces;

public interface IBooksService
{
    Task<BookDto> GetBook(int id);
    List<BookDto> GetAllBooks();
    Task<bool> AddBook(BookDto book);
    List<BookDto> GetAuthorBooks(int authorId);
}