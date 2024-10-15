using Application.DTOs;
using Application.UseCases.Commands;
using AutoMapper;
using Domain.Repository;
using MediatR;

namespace Application.UseCases.CommandHandlers;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var foundBook = await _bookRepository.GetByIdAsync(request.Id);
        if (foundBook is not null)
        {
            _mapper.Map(request, foundBook);
            await _bookRepository.UpdateAsync(foundBook);
        }

        return _mapper.Map<BookDto>(foundBook);
    }
}