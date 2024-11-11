using Application.DTOs;
using Application.UseCases.Queries;
using AutoMapper;
using Domain.Repository;
using MediatR;

namespace Application.UseCases.QueryHandlers;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    
    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await _bookRepository.GetAllAsync();
        return _mapper.Map<List<BookDto>>(books);
    }
}