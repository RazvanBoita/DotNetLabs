using System.Net.Http.Json;
using Application.UseCases.Commands;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookManagement.IntegrationTests;

public class BooksControllerTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ApplicationDbContext _dbContext;
    private string BaseUrl = "/api/Books";
    private Guid _guid = Guid.Parse("d263b12d-ec6f-4774-879c-bf124875ea41");
    public BooksControllerTests(WebApplicationFactory<Program> factory)
    {
        this._factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor =
                    services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                services.Remove(descriptor);
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });
            });
        });

        var scope = _factory.Services.CreateScope();
        _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _dbContext.Database.EnsureCreated();
    }


    [Fact]
    public async void GivenBooks_WhereGetByIdIsCalled_ThenReturnsTheRightContentType()
    {
        //TODO Arrange

        var client = _factory.CreateClient();

        //TODO Act

        var response = await client.GetAsync($"api/Books?Id=d263b12d-ec6f-4774-879c-bf124875ea41");

        //TODO Assert
        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
        // response.Result.EnsureSuccessStatusCode();
        // response.Result.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
    }

    [Fact]
    public async void GivenValidBook_WhenCreatedIsCalled_Then_ShouldAddInDb()
    {
        //TODO Arrange

        var client = _factory.CreateClient();

        //TODO Act

        var command = new CreateBookCommand
        {
            Title = "Title 1",
            Author = "Author 1",
            ISBN = "1234",
            PublicationDate = new DateTime(2021, 1,1 )
        };

        await client.PostAsJsonAsync(BaseUrl, command);
        
        //TODO Assert
        var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Title == command.Title);
        book.Should().NotBeNull();
        book.Title.Should().Be(command.Title);
    }
    
    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }

    private void CreateBook()
    {
        var book = new Book
        {
            Title = "Title",
            Author = "Author",
            ISBN = "12345",
            PublicationDate = DateTime.Today
        };
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges();
    }
}