using Dal;
using Domain.Dtos;
using Services.Interfaces;

namespace Services;

public class SearchService : ISearchService
{
    private readonly BookStoreDbContext _db;

    public SearchService(BookStoreDbContext db)
    {
        _db = db;
    }

    public List<SearchResultDto> Search(string query)
    {
        var results = new List<SearchResultDto>();

        if (!string.IsNullOrEmpty(query))
        {
            var booksQuery = _db.Books.AsQueryable();
            var authorsQuery = _db.Authors.AsQueryable();

            var bookResults = booksQuery.Where(b =>
                    b.Name.Contains(query) ||
                    b.Description.Contains(query)
                // Add more fields as necessary
            ).Select(b => new SearchResultDto
            {
                Entity = "book",
                Id = b.Id,
                Name = b.Name,
                // Add other properties as needed
            });

            var authorResults = authorsQuery.Where(a =>
                    a.Name.Contains(query)
                // Add more fields as necessary
            ).Select(a => new SearchResultDto
            {
                Entity = "author",
                Id = a.Id,
                Name = a.Name
                // Add other properties as needed
            });

            results.AddRange(bookResults);
            results.AddRange(authorResults);
        }

        return results;
    }
}