using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace Services.Implementations;

public class PropertiesService : IPropertiesService
{
    private readonly IPropertiesRepository _propertiesRepository;

    public PropertiesService(IPropertiesRepository propertiesRepository)
    {
        _propertiesRepository = propertiesRepository;
    }

    public async Task<IEnumerable<Property>> GetProperties()
    {
        var properties = await _propertiesRepository.Get();
        return properties;
    }

    public async Task<Property> GetById(Guid id)
    {
        return await _propertiesRepository.GetById(id);
    }

    public async Task<bool> DeleteProperty(Guid id)
    {
        return await _propertiesRepository.Delete(id);
    }

    public async Task<Property> CreateProperty(Property entity)
    {
        return await _propertiesRepository.Create(entity);
    }

    public async Task<bool> UpdateProperty(Property entity)
    {
        return await _propertiesRepository.Update(entity);
    }

    public async Task<List<Property>> GetFilteredProperties(List<int> rooms, decimal? priceMin, decimal? priceMax, string street = null, string city = null,
        string state = null, string country = null, string zipCode = null)
    {
        return await _propertiesRepository.GetFilteredProperties(rooms, priceMin, priceMax, street, city, state,
            country, zipCode);
    }
}