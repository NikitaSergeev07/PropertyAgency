using System.Collections;
using PropertyAgency.Domain.Entities;

namespace Services.Interfaces;

public interface IRentalsService
{
    Task<IEnumerable<Rental>> GetRentals();

    Task<Rental> GetById(Guid id);
    Task<bool> DeleteRental(Guid id);
    Task<Rental> CreateRental(Rental entity);
    Task<bool> UpdateRental(Rental entity);

    Task<IEnumerable<Rental>> GetByBetweenDate(DateTime startDate, DateTime endDate);
}