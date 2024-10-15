using MediatR;

namespace Application.UseCases.Commands;

public class CreateBookCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public DateTime PublicationDate { get; set; }
    
}