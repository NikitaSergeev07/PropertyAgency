using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IOperationsRepository : IBaseRepository<Operation>
{
    Task<List<Operation>> GetByDate(DateTime OperationDate);
    Task<List<Operation>> GetByAmount(Decimal OperationAmount);
}