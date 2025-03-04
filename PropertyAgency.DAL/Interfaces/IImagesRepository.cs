using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IImagesRepository : IBaseRepository<Image>
{
    Task<List<Image>> GetByProperty(Guid Property_id);
}