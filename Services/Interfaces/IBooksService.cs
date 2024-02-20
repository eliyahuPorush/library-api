using Domain.Dtos;

namespace Services.Interfaces;

public interface IBooksService
{
    Task<BookDto> GetBook(int id);
    List<BookDto> GetAllBooks();
    Task<int> AddBook(BookDto book);
    List<BookDto> GetAuthorBooks(int authorId);
    Task DeleteBook(string id);
    Task UpdateBook(int id, BookDto book);
}