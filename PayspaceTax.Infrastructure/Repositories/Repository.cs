using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Infrastructure.Database;

namespace PayspaceTax.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T: class
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<T> GetAll()
    {
        return _context.Set<T>();
    }

    public async Task<T> AddAsync(T entity)
    {
        var entityEntry = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }
    
    public async Task<T?> GetFirstByAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }
}