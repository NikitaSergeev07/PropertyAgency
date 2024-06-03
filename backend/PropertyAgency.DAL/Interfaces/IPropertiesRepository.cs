using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IPropertiesRepository : IBaseRepository<Property>
{
    Task<List<Property>> GetFilteredProperties(List<int> rooms, decimal? priceMin, decimal? priceMax,
        string street = null, string city = null, string state = null, string country = null, string zipCode = null);
}