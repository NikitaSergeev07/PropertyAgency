using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers;

[ApiController]
[Route("[controller]")]
public class OperationsController : ControllerBase
{
    private readonly IOperationsService _operationsService;
    public OperationsController(IOperationsService operationsService)
    {
        _operationsService = operationsService;
    }
    
    private Operation MapCustomerObject(OperationDto payload)
    {
        var result = new Operation();
        result.OperationDate = payload.OperationDate;
        result.Amount = payload.Amount;
        result.SellerId= payload.SellerId;
        result.PropertyId = payload.PropertyId;
        result.BuyerId= payload.BuyerId;
        return result;
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetOperations()
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var operations = await _operationsService.GetOperations();

        return Ok(operations);
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
        var operation = await _operationsService.GetById(id);
        return Ok(operation);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateOperation(OperationDto operation)
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var newOperation = MapCustomerObject(operation);
        await _operationsService.CreateOperation(newOperation);
        return Created($"/Transaction/{newOperation.Id}", newOperation);
    }
    [HttpPatch("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateOperation(Guid id, OperationDto operation)
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var updateOperation = MapCustomerObject(operation);
        updateOperation.Id = id;
        await _operationsService.UpdateOperation(updateOperation);
        return Ok(updateOperation);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteOperation(Guid id)
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        return Ok(await _operationsService.DeleteOperation(id));
    }

    [HttpGet("by-Date")]
    [Authorize]
    public async Task<IActionResult> GetByBetweenDate([FromQuery] DateTime Date)
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var operations = await _operationsService.GetOperationsByDate(Date);
        return Ok(operations);
    }
    [HttpGet("by-Amount")]
    [Authorize]
    public async Task<IActionResult> GetOperationsByAmount([FromQuery] Decimal Amount)
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }
        var operations = await _operationsService.GetOperationsByAmount(Amount);
        return Ok(operations);
    }
}