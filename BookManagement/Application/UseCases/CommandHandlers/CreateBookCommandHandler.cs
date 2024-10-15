using Application.UseCases.Commands;
using Domain.Repository;
using MediatR;

namespace Application.UseCases.CommandHandlers;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Domain.Entities.Book
        {
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            PublicationDate = request.PublicationDate
        };
        return await _bookRepository.AddAsync(book);
    }
}