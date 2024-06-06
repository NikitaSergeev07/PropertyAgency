using PropertyAgency.Domain.Entities;

namespace Services.Interfaces;

public interface IPropertiesService
{
    Task<IEnumerable<Property>> GetProperties();

    Task<Property> GetById(Guid id);
    Task<bool> DeleteProperty(Guid id);
    Task<Property> CreateProperty(Property entity);
    Task<bool> UpdateProperty(Property entity);

    Task<List<Property>> GetFilteredProperties(List<int> rooms, decimal? priceMin, decimal? priceMax,
        string street = null, string city = null, string state = null, string country = null, string zipCode = null);
}