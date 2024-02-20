using Domain.Dtos;

namespace Services.Interfaces;

public interface IBooksService
{
    Task<BookDto> GetBook(int id);
    List<BookDto> GetAllBooks();
    Task<int> AddBook(BookDto book);
    List<BookDto> GetAuthorBooks(int authorId);
    Task DeleteBook(int id);
    Task UpdateBook(BookDto book);
}