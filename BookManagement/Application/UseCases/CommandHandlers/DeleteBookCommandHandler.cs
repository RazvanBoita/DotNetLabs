using Application.DTOs;
using Application.UseCases.Commands;
using AutoMapper;
using Domain.Repository;
using MediatR;

namespace Application.UseCases.CommandHandlers;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public DeleteBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    
    public async Task<BookDto> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.DeleteAsync(request.Id);
        return _mapper.Map<BookDto>(book);
    }
}