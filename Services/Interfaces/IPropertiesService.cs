using PropertyAgency.Domain.Entities;

namespace Services.Interfaces;

public interface IPropertiesService
{
    Task<IEnumerable<Property>> GetProperties();

    Task<Property> GetById(Guid id);
    Task<bool> DeleteProperty(Guid id);
    Task<bool> CreateProperty(Property entity);
    Task<bool> UpdateProperty(Property entity);
}