using Application.DTOs;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoodBookManagement.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateBookCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<BookDto>> GetBook([FromQuery] GetBookByIdQuery query)
    {
        return await _mediator.Send(query);
    }
    
    [HttpDelete]
    public async Task<ActionResult<BookDto>> DeleteBook([FromQuery] DeleteBookCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [HttpPut]
    public async Task<ActionResult<BookDto>> UpdateBook([FromQuery] Guid id, [FromBody] UpdateBookCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Ids dont match...");
        }
        return await _mediator.Send(command);
    }
}