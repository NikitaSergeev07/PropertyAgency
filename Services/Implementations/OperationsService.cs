using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;
using Operation = Microsoft.AspNetCore.JsonPatch.Operations.Operation;

namespace Services.Implementations;

public class OperationsService : IOperationsService
{
    private readonly IOperationsRepository _operationsRepository;

    public OperationsService(IOperationsRepository operationsRepository)
    {
        _operationsRepository = operationsRepository;
    }


    public async Task<List<PropertyAgency.Domain.Entities.Operation>> GetOperations()
    {
        return await _operationsRepository.Get();
    }

    public async Task<PropertyAgency.Domain.Entities.Operation> GetById(Guid id)
    {
        return await _operationsRepository.GetById(id);
    }

    public async Task<bool> DeleteOperation(Guid id)
    {
        return await _operationsRepository.Delete(id);
    }

    public async Task<PropertyAgency.Domain.Entities.Operation> CreateOperation(PropertyAgency.Domain.Entities.Operation entity)
    {
        return await _operationsRepository.Create(entity);
    }

    public async Task<bool> UpdateOperation(PropertyAgency.Domain.Entities.Operation entity)
    {
        return await _operationsRepository.Update(entity);
    }

    public async Task<List<PropertyAgency.Domain.Entities.Operation>> GetOperationsByDate(DateTime Date)
    {
        return await _operationsRepository.GetByDate(Date);
    }

    public async Task<List<PropertyAgency.Domain.Entities.Operation>> GetOperationsByAmount(decimal Amount)
    {
        return await _operationsRepository.GetByAmount(Amount);
    }
}