using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IRentalsRepository : IBaseRepository<Rental>
{
    Task<Rental> GetByBetweenDate(Guid id);
}