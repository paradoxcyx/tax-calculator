namespace PayspaceTax.Application.Interfaces.Repositories;

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

}