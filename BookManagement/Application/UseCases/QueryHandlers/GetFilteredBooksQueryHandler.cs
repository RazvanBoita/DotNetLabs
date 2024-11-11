using Application.DTOs;
using AutoMapper;
using Domain.Repository;
using Gridify;
using MediatR;

namespace Application.UseCases.QueryHandlers;

public class GetFilteredBooksQueryHandler : IRequestHandler<GetFilteredBooksQuery, List<BookDto>>
{
    private readonly IMapper _mapper;
    private readonly IBookRepository _bookRepository;

    public GetFilteredBooksQueryHandler(IMapper mapper, IBookRepository bookRepository)
    {
        _mapper = mapper;
        _bookRepository = bookRepository;
    }
    
    public async Task<List<BookDto>> Handle(GetFilteredBooksQuery request, CancellationToken cancellationToken)
    {
        // var books = await _bookRepository.GetAllAsync();
        // var gridifyQuery = new GridifyQuery
        // {
        //     Filter = request.Filter,
        //     Page = request.Page,
        //     PageSize = request.PageSize,
        // };
        // var gridifyResult = books.AsQueryable().ApplyFiltering(gridifyQuery);
        //
        return new List<BookDto>();
    }
}