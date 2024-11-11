using Application.DTOs;
using Application.UseCases.CommandHandlers;
using Application.UseCases.Commands;
using AutoMapper;
using Domain.Repository;
using NSubstitute;
using Domain.Entities;

public class DeleteBookCommandHandlerTests
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    private readonly DeleteBookCommandHandler _handler;

    public DeleteBookCommandHandlerTests()
    {
        _bookRepository = Substitute.For<IBookRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new DeleteBookCommandHandler(_bookRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldReturnBookDto_WhenBookIsDeleted()
    {
        // Arrange
        var bookId = new Guid("618e5010-7b8d-4c7c-b25c-8d62609e39c2");
        var book = new Book { Id = bookId };
        var bookDto = new BookDto { Id = bookId };

        _bookRepository.DeleteAsync(bookId).Returns(book);
        _mapper.Map<BookDto>(book).Returns(bookDto);

        var command = new DeleteBookCommand { Id = bookId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(bookId, result.Id);
        await _bookRepository.Received(1).DeleteAsync(bookId);
        _mapper.Received(1).Map<BookDto>(book);
    }
}
