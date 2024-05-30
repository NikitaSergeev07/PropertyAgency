using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertiesService _propertiesService;
    public PropertiesController(IPropertiesService propertiesService)
    {
        _propertiesService = propertiesService;
    }
    
    private Property MapCustomerObject(PropertyDto payload)
    {
        var result = new Property();
        result.Title = payload.Title;
        result.Description = payload.Description;
        result.Price = payload.Price;
        result.RoomCount = payload.RoomCount;
        result.Status = payload.Status;
        result.UserId = payload.UserId;
        return result;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetProperties()
    {
        var properties = await _propertiesService.GetProperties();
        
        return Ok(properties);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var property = await _propertiesService.GetById(id);
        return Ok(property);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateProperty(PropertyDto property)
    {
        var newProperty = MapCustomerObject(property);
        await _propertiesService.CreateProperty(newProperty);
        return Created($"/property/{newProperty.Id}", newProperty);
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProperty(Guid id, PropertyDto property)
    {
        var updateProperty = MapCustomerObject(property);
        updateProperty.Id = id;
        await _propertiesService.UpdateProperty(updateProperty);
        return Ok(updateProperty);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProperty(Guid id)
    {
        return Ok(await _propertiesService.DeleteProperty(id));
    
    }
    
    
}