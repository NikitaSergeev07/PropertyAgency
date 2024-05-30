using PropertyAgency.Domain.Entities;

namespace Services.Interfaces;

public interface IAddressesService
{
    Task<IEnumerable<Address>> GetAddresses();

    Task<Address> GetById(Guid id);
    Task<bool> DeleteAddress(Guid id);
    Task<Address> CreateAddress(Address entity);
    Task<bool> UpdateAddress(Address entity);
    Task<Address> GetByPropertyId(Guid propertyId);
    
}