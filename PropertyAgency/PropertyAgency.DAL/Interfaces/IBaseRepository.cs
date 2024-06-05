namespace PropertyAgency.DAL.Interfaces;

public interface IBaseRepository<T>
{
    Task<T> Create(T entity);
    Task<T> GetById(Guid id);
    Task<List<T>> Get();
    Task<bool> Delete(Guid id);
    Task<bool> Update(T entity);
}