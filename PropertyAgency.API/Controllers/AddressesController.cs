using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers;


[ApiController]
[Route("[controller]")]
public class AddressesController : ControllerBase
{
    private readonly IAddressesService _addressesService;
    public AddressesController(IAddressesService addressesService)
    {
        _addressesService = addressesService;
    }
    
    private Address MapCustomerObject(AddressDto payload)
    {
        var result = new Address();
        result.Street = payload.Street;
        result.City = payload.City;
        result.State = payload.State;
        result.Country = payload.Country;
        result.ZipCode = payload.ZipCode;
        return result;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAddresses()
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var addresses = await _addressesService.GetAddresses();
        
        return Ok(addresses);
    }
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var address = await _addressesService.GetById(id);
        return Ok(address);
    }
    
    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> CreateAddress(AddressDto address)
    {
        var newAddress = MapCustomerObject(address);
        await _addressesService.CreateAddress(newAddress);
        return Created($"/address/{newAddress.Id}", newAddress);
    }
    [HttpPatch("{id:guid}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> UpdateAddress(Guid id, AddressDto address)
    {
        var updateAddress = MapCustomerObject(address);
        updateAddress.Id = id;
        await _addressesService.UpdateAddress(updateAddress);
        return Ok(updateAddress);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteAddress(Guid id)
    {
        return Ok(await _addressesService.DeleteAddress(id));
    }
    

}