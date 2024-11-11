using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using Domain.Repository;
using Domain.Utils;
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

    public async Task<Result<Guid>> AddAsync(Book book)
    {
        try
        {
            await _applicationDbContext.AddAsync(book);
            await _applicationDbContext.SaveChangesAsync();
            return Result<Guid>.Success(book.Id);
        }
        catch (Exception e)
        {
            return Result<Guid>.Failure(e.Message);
        }
    }

    public async Task<Result<Book>> UpdateAsync(Book book)
    {
        try
        {
            _applicationDbContext.Entry(book).State = EntityState.Modified;
            await _applicationDbContext.SaveChangesAsync();
            return Result<Book>.Success(book);
        }
        catch (Exception e)
        {
            return Result<Book>.Failure("Book update failed");
        }        
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