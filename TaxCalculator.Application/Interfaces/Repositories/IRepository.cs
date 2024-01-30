using System.Linq.Expressions;

namespace TaxCalculator.Application.Interfaces.Repositories;

public interface IRepository<T>
{
    /// <summary>
    /// Retrieve all records from database
    /// </summary>
    /// <returns></returns>
    IQueryable<T> GetAll();

    /// <summary>
    /// Add record to database
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Created Entity</returns>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Add records to database
    /// </summary>
    /// <param name="entities">Entities</param>
    /// <returns></returns>
    Task AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Retrieve the first record by predicate
    /// </summary>
    /// <param name="predicate">Lookup predicate</param>
    /// <returns>The entity</returns>
    Task<T?> GetFirstByAsync(Expression<Func<T, bool>> predicate);

}