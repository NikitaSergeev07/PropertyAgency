using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace Services.Implementations;

public class RentalsService : IRentalsService
{
    private readonly IRentalsRepository _rentalsRepository;

    public RentalsService(IRentalsRepository rentalsRepository)
    {
        _rentalsRepository = rentalsRepository;
    }
    
    public async Task<IEnumerable<Rental>> GetRentals()
    {
        return await _rentalsRepository.Get();
    }

    public async Task<Rental> GetById(Guid id)
    {
        return await _rentalsRepository.GetById(id);
    }

    public async Task<bool> DeleteRental(Guid id)
    {
        return await _rentalsRepository.Delete(id);
    }

    public async Task<Rental> CreateRental(Rental entity)
    {
        return await _rentalsRepository.Create(entity);
    }

    public async Task<bool> UpdateRental(Rental entity)
    {
        return await _rentalsRepository.Update(entity);
    }

    public async Task<IEnumerable<Rental>> GetByBetweenDate(DateTime startDate, DateTime endDate)
    {
        return await _rentalsRepository.GetByBetweenDate(startDate, endDate);
    }
}