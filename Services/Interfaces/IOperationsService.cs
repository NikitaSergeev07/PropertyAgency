using Microsoft.AspNetCore.JsonPatch.Operations;

namespace Services.Interfaces;

public interface IOperationsService
{
    Task<List<PropertyAgency.Domain.Entities.Operation>> GetOperations();

    Task<PropertyAgency.Domain.Entities.Operation> GetById(Guid id);
    Task<bool> DeleteOperation(Guid id);
    Task<PropertyAgency.Domain.Entities.Operation> CreateOperation(PropertyAgency.Domain.Entities.Operation entity);
    Task<bool> UpdateOperation(PropertyAgency.Domain.Entities.Operation entity);

    Task<List<PropertyAgency.Domain.Entities.Operation>> GetOperationsByDate(DateTime Date);
    Task<List<PropertyAgency.Domain.Entities.Operation>> GetOperationsByAmount(decimal Amount);
}