using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IAddressesRepository : IBaseRepository<Address>
{
    Task<Address> GetByPropertyId(Guid propertyId);
}