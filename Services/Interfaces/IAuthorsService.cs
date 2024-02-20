using Domain.Dtos;

namespace Services.Interfaces;

public interface IAuthorsService
{
    List<AuthorDto> GetAuthors();
    Task<int> AddAuthorAsync(AuthorDto author);
    Task<int> DeleteAuthorAsync(int authorId);
    Task UpdateAuthor(AuthorDto author);
}