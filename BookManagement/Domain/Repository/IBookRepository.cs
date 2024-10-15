using Domain.Entities;

namespace Domain.Repository;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task<Book> DeleteAsync(Guid id);
}