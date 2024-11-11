using Application.DTOs;
using Application.UseCases.Queries;
using Application.UseCases.QueryHandlers;
using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using NSubstitute;

namespace BookManagement.Application.UnitTests;

public class GetBooksQueryHandlerTest
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public GetBooksQueryHandlerTest()
    {
        _bookRepository = Substitute.For<IBookRepository>();
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public void Given_GetBooksQueryHandler_When_HandleIsCalled_Then_AListOfBooksShouldBeReturned()
    {
        //TODO Arrange
        var books = GenerateBooks();
        _bookRepository.GetAllAsync().Returns(books);
        var query = new GetBooksQuery();
        GenerateBooksDto(books);
        //TODO Act

        var handler = new GetBooksQueryHandler(_bookRepository, _mapper);
        var result = handler.Handle(query, CancellationToken.None).Result;
        //TODO Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    private void GenerateBooksDto(List<Book> books)
    {
        _mapper.Map<List<BookDto>>(books).Returns(new List<BookDto>
        {
            new BookDto
            {
                Id = books[0].Id,
                Title = books[0].Title,
                Author = books[0].Author,
                ISBN = books[0].ISBN
            },
            new BookDto
            {
                Id = books[1].Id,
                Title = books[1].Title,
                Author = books[1].Author,
                ISBN = books[1].ISBN
            }
        });
    }

    private List<Book> GenerateBooks()
    {
        return new List<Book>
        {
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Book 1",
                Author = "Author 1",
                ISBN = "ISBN 1",
                PublicationDate = DateTime.Now
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Book 2",
                Author = "Author 2",
                ISBN = "ISBN 2",
                PublicationDate = DateTime.Now
            }
        };
    }
}