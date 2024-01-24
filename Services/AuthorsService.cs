using AutoMapper;
using Dal;
using Dal.Models;
using Domain.Dtos;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services;

public class AuthorsService : IAuthorsService
{
    private readonly BookStoreDbContext _db;
    private readonly IMapper _mapper;

    public AuthorsService(BookStoreDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public List<AuthorDto> GetAuthors()
    {
        return _mapper.Map<List<AuthorDto>>(_db.Authors);
    }

    public async Task<int> AddAuthorAsync(AuthorDto author)
    {
        var mappedAuthor = _mapper.Map<Author>(author);
        try
        {
            var addedAuthor = await _db.Authors.AddAsync(mappedAuthor);
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
            return authorId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
}