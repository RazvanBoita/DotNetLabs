using Application.UseCases.Commands;
using Domain.Repository;
using Domain.Utils;
using FluentValidation;
using MediatR;

namespace Application.UseCases.CommandHandlers;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<Guid>>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Domain.Entities.Book
        {
            Title = request.Title,
            Author = request.Author,
            ISBN = request.ISBN,
            PublicationDate = request.PublicationDate
        };
        //validation data poate
        var result = await _bookRepository.AddAsync(book);
        if (result.IsSuccess)
        {
            return Result<Guid>.Success(result.Data);
        }

        return Result<Guid>.Failure(result.ErrorMessage);
    }
}