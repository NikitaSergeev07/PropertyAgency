namespace PropertyAgency.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<bool> Create(T entity);
    Task<T> GetById(Guid id);
    Task<List<T>> Get();
    Task<bool> Delete(Guid id);
    Task<bool> Update(T entity);
}