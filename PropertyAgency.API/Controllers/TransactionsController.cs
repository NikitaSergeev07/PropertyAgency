using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers;


[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsService _transactionsService;
    public TransactionsController(ITransactionsService transactionsService)
    {
        _transactionsService = transactionsService;
    }
    
    private Transaction MapCustomerObject(TransactionDto payload)
    {
        var result = new Transaction();
        result.TransactionDate = payload.TransactionDate;
        result.Amount = payload.Amount;
        result.SellerId= payload.SellerId;
        result.PropertyId = payload.PropertyId;
        result.BuyerId= payload.BuyerId;
        return result;
    }
    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        var transactions = await _transactionsService.GetTransactions();

        return Ok(transactions);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var transaction = await _transactionsService.GetById(id);
        return Ok(transaction);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTransaction(TransactionDto transaction)
    {
        var newTransaction = MapCustomerObject(transaction);
        await _transactionsService.CreateTransaction(newTransaction);
        return Created($"/Transaction/{newTransaction.Id}", newTransaction);
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTransaction(Guid id, TransactionDto transaction)
    {
        var updateTransaction = MapCustomerObject(transaction);
        updateTransaction.Id = id;
        await _transactionsService.UpdateTransaction(updateTransaction);
        return Ok(updateTransaction);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTransaction(Guid id)
    {
        return Ok(await _transactionsService.DeleteTransaction(id));
    }

    [HttpGet("by-Date")]
    public async Task<IActionResult> GetByBetweenDate([FromQuery] DateTime Date)
    {
        var transactions = await _transactionsService.GetTransactionsByDate(Date);
        return Ok(transactions);
    }
    [HttpGet("by-Amount")]
    public async Task<IActionResult> GetByBetweenDate([FromQuery] Decimal Amount)
    {
        var transactions = await _transactionsService.GetTransactionsByAmount(Amount);
        return Ok(transactions);
    }
}