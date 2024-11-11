using Application.DTOs;
using Domain.Utils;
using MediatR;

namespace Application.UseCases.Commands;

public class UpdateBookCommand : IRequest<Result<Unit>>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public DateTime PublicationDate { get; set; }
    public Guid Id { get; set; }
}