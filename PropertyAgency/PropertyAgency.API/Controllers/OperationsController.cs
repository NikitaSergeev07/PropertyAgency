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
    public async Task<IActionResult> GetOperations()
    {
        var operations = await _operationsService.GetOperations();

        return Ok(operations);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var operation = await _operationsService.GetById(id);
        return Ok(operation);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOperation(OperationDto operation)
    {
        var newOperation = MapCustomerObject(operation);
        await _operationsService.CreateOperation(newOperation);
        return Created($"/Transaction/{newOperation.Id}", newOperation);
    }
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateOperation(Guid id, OperationDto operation)
    {
        var updateOperation = MapCustomerObject(operation);
        updateOperation.Id = id;
        await _operationsService.UpdateOperation(updateOperation);
        return Ok(updateOperation);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOperation(Guid id)
    {
        return Ok(await _operationsService.DeleteOperation(id));
    }

    [HttpGet("by-Date")]
    public async Task<IActionResult> GetByBetweenDate([FromQuery] DateTime Date)
    {
        var operations = await _operationsService.GetOperationsByDate(Date);
        return Ok(operations);
    }
    [HttpGet("by-Amount")]
    public async Task<IActionResult> GetOperationsByAmount([FromQuery] Decimal Amount)
    {
        var operations = await _operationsService.GetOperationsByAmount(Amount);
        return Ok(operations);
    }
}