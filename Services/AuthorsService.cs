using AutoMapper;
using Dal;
using Dal.Models;
using Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Interfaces;

namespace Services;

public class AuthorsService : IAuthorsService
{
    private readonly BookStoreDbContext _db;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthorsService> _logger;

    public AuthorsService(BookStoreDbContext db, IMapper mapper, ILogger<AuthorsService> logger)
    {
        _db = db;
        _mapper = mapper;
        _logger = logger;
    }

    public List<AuthorDto> GetAuthors()
    {
        return _mapper.Map<List<AuthorDto>>(_db.Authors.Include(a => a.Address));
    }

    public async Task<int> AddAuthorAsync(AuthorDto author)
    {
        var mappedAuthor = _mapper.Map<Author>(author);
        try
        {
            var addedAuthor = await _db.Authors.AddAsync(mappedAuthor);
            _logger.LogInformation($"[AuthorsService][AddAuthorAsync] About to add new author to db, added aurhor id: {addedAuthor.Entity.Id}");
            await _db.SaveChangesAsync();
            return addedAuthor.Entity.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> DeleteAuthorAsync(int authorId)
    {
        if (authorId <= 0)
        {
            throw new InvalidOperationException("author id is invalid");
        }

        try
        {
            var author = await _db.Authors.FindAsync(authorId);

            if (author is null)
            {
                throw new InvalidOperationException("author not found");
            }

            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();
            _logger.LogInformation($"[AuthorsService][DeleteAuthorAsync] Author with id {author.Id} has been deleted");
            return authorId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task UpdateAuthor(int id, AuthorDto author)
    {
        var authorToUpdate = _db.Authors.FindAsync(id);
        _db.Entry(authorToUpdate).CurrentValues.SetValues(authorToUpdate);
        await _db.SaveChangesAsync();
    }
}