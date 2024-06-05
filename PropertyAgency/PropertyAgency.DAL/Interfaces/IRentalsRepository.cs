using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IRentalsRepository : IBaseRepository<Rental>
{
    Task<IEnumerable<Rental>> GetByBetweenDate(DateTime startDate, DateTime endDate);
}