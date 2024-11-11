using Domain.Entities;
using Domain.Utils;

namespace Domain.Repository;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();
    Task<Book?> GetByIdAsync(Guid id);
    Task<Result<Guid>> AddAsync(Book book);
    Task<Result<Book>> UpdateAsync(Book book);
    Task<Book> DeleteAsync(Guid id);
}