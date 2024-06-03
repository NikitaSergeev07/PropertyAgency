using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers;


[ApiController]
[Route("[controller]")]
public class RentalsController : ControllerBase
{
    private readonly IRentalsService _rentalsService;
    public RentalsController(IRentalsService rentalsService)
    {
        _rentalsService = rentalsService;
    }
    
    private Rental MapCustomerObject(RentalDto payload)
    {
        var result = new Rental();
        result.StartDate = payload.StartDate;
        result.EndDate = payload.EndDate;
        result.RentAmount = payload.RentAmount;
        result.PropertyId = payload.PropertyId;
        result.RenterId = payload.RenterId;
        return result;
    }
    [HttpGet]
    public async Task<IActionResult> GetRentals()
    {
        var rentals = await _rentalsService.GetRentals();

        return Ok(rentals);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var rental = await _rentalsService.GetById(id);
        return Ok(rental);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateRental(RentalDto rental)
    {
        var newRental = MapCustomerObject(rental);
        await _rentalsService.CreateRental(newRental);
        return Created($"/rental/{newRental.Id}", newRental);
    }
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateRental(Guid id, RentalDto rental)
    {
        var updateRental = MapCustomerObject(rental);
        updateRental.Id = id;
        await _rentalsService.UpdateRental(updateRental);
        return Ok(updateRental);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteRental(Guid id)
    {
        return Ok(await _rentalsService.DeleteRental(id));
    }

    [HttpGet("by-betweenDate")]
    public async Task<IActionResult> GetByBetweenDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var rentals = await _rentalsService.GetByBetweenDate(startDate, endDate);
        return Ok(rentals);
    }
}