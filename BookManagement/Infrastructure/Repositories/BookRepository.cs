using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ApplicationDbContext _applicationDbContext;
    public BookRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _applicationDbContext.Books.ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid guid)
    {
        return await _applicationDbContext.Books.FindAsync(guid); 
    }

    public async Task<Guid> AddAsync(Book book)
    {
        await _applicationDbContext.AddAsync(book);
        await _applicationDbContext.SaveChangesAsync();
        return book.Id;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        var foundBook = await GetByIdAsync(book.Id);
        if (foundBook is not null)
        {
            _applicationDbContext.Entry(foundBook).CurrentValues.SetValues(book);
            await _applicationDbContext.SaveChangesAsync();
        }

        return foundBook;
    }

    public async Task<Book> DeleteAsync(Guid id)
    {
        var foundBook = await GetByIdAsync(id);
        if (foundBook is not null)
        {
            _applicationDbContext.Remove(foundBook);
            await _applicationDbContext.SaveChangesAsync();
        }
        return foundBook;
    }
}