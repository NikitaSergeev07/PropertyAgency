using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> GetRentals()
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var rentals = await _rentalsService.GetRentals();

        return Ok(rentals);
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
        var rental = await _rentalsService.GetById(id);
        return Ok(rental);
    }
    
    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> CreateRental(RentalDto rental)
    {
        var newRental = MapCustomerObject(rental);
        await _rentalsService.CreateRental(newRental);
        return Created($"/rental/{newRental.Id}", newRental);
    }
    [HttpPatch("{id:guid}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> UpdateRental(Guid id, RentalDto rental)
    {
        var updateRental = MapCustomerObject(rental);
        updateRental.Id = id;
        await _rentalsService.UpdateRental(updateRental);
        return Ok(updateRental);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> DeleteRental(Guid id)
    {
        return Ok(await _rentalsService.DeleteRental(id));
    }

    [HttpGet("by-betweenDate")]
    [Authorize]
    public async Task<IActionResult> GetByBetweenDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var rentals = await _rentalsService.GetByBetweenDate(startDate, endDate);
        return Ok(rentals);
    }
}