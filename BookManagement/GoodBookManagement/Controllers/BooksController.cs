using Application;
using Application.DTOs;
using Application.UseCases.Commands;
using Application.UseCases.Queries;
using Domain.Utils;
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
    public async Task<ActionResult<Result<Guid>>> Create(CreateBookCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpGet]
    public async Task<ActionResult<List<BookDto>>> GetBooks()
    {
        var query = new GetBooksQuery();
        return await _mediator.Send(query);
    }
    
    // [HttpGet]
    // public async Task<ActionResult<BookDto>> GetBook([FromQuery] GetBookByIdQuery query)
    // {
    //     return await _mediator.Send(query);
    // }
    
    [HttpDelete]
    public async Task<ActionResult<BookDto>> DeleteBook([FromQuery] DeleteBookCommand command)
    {
        return await _mediator.Send(command);
    }
    
    // [HttpPut]
    // public async Task<IActionResult> UpdateBook([FromQuery] Guid id, [FromBody] UpdateBookCommand command)
    // {
    //     if (id != command.Id)
    //     {
    //         return BadRequest("Ids dont match...");
    //     }
    //     var result = await _mediator.Send(command);
    //
    //     return result.Match<IActionResult>(
    //         onSuccess: () => NoContent(),
    //         onFailure: error => BadRequest(error.Description)
    //     );
    //
    //     // return NoContent(); acelasi lucru cu:
    //     // return StatusCode(StatusCodes.Status204NoContent);
    // }
}