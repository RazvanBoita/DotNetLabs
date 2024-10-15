using Application.DTOs;
using Application.UseCases.Queries;
using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using MediatR;

namespace Application.UseCases.QueryHandlers;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    
    public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id);
        return _mapper.Map<BookDto>(book);
    }
}