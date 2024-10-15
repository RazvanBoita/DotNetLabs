using Application.DTOs;
using MediatR;

namespace Application.UseCases.Commands;

public class DeleteBookCommand : IRequest<BookDto>
{
    public Guid Id { get; set; }
}