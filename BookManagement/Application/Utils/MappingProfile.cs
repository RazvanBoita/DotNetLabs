using Application.DTOs;
using Application.UseCases.Commands;
using AutoMapper;
using Domain.Entities;

namespace Application.Utils;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<Book, BookDto>()
            .ReverseMap();
        //reverse map e si pt ordinea inversa (din dto in book)

        CreateMap<UpdateBookCommand, Book>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        //aici fac asta ca sa nu se modifice campurile care sunt null din request, altfel ar da eroare
    }
}