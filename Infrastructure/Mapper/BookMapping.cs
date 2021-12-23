using Application.DTOs.Books;
using AutoMapper;
using Core.Books;

namespace Infrastructure.Mapper;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book<long>, BookDTO<long>>().ReverseMap();
        CreateMap<CreateBookDTO, Book<long>>();
    }
}