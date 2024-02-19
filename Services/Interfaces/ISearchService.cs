using Domain.Dtos;

namespace Services.Interfaces;

public interface ISearchService
{
    List<SearchResultDto> Search(string query);
}