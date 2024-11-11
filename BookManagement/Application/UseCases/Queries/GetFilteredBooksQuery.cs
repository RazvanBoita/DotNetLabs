using Application.DTOs;
using Gridify;
using MediatR;

namespace Application;

public class GetFilteredBooksQuery : IRequest<List<BookDto>>
{
    public string Filter { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}