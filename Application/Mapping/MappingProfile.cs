using AutoMapper;
using Dal.Models;
using Domain.Dtos;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Author, AuthorDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}