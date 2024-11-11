using System.Runtime.InteropServices.JavaScript;
using Application.DTOs;
using Application.UseCases.Commands;
using AutoMapper;
using Domain.Repository;
using Domain.Utils;
using FluentValidation;
using MediatR;

namespace Application.UseCases.CommandHandlers;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result<Unit>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public UpdateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<Unit>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateBookCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return Result<Unit>.Failure(validationResult.Errors.ToString());
        }
        
        var foundBook = await _bookRepository.GetByIdAsync(request.Id);
        if (foundBook is null)
        {
            return Result<Unit>.Failure("Book not found");
        }

        try
        {
            _mapper.Map(request, foundBook);
            await _bookRepository.UpdateAsync(foundBook);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception e)
        {
            return Result<Unit>.Failure(e.Message);
        }
    }
}