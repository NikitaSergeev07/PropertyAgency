using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace Services.Implementations;

public class AddressesService : IAddressesService
{
    private readonly IAddressesRepository _addressesRepository;

    public AddressesService(IAddressesRepository addressesRepository)
    {
        _addressesRepository = addressesRepository;
    }
    
    public async Task<IEnumerable<Address>> GetAddresses()
    {
        var addresses = await _addressesRepository.Get();
        return addresses;
    }

    public async Task<Address> GetById(Guid id)
    {
        return await _addressesRepository.GetById(id);
    }

    public async Task<bool> DeleteAddress(Guid id)
    {
        await _addressesRepository.Delete(id);
        return true;
    }

    public async Task<Address> CreateAddress(Address entity)
    {
        return await _addressesRepository.Create(entity);
    }

    public async Task<bool> UpdateAddress(Address entity)
    {
        return await _addressesRepository.Update(entity);
    }
    
}