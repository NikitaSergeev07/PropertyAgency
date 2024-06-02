using PropertyAgency.Domain.Entities;

namespace PropertyAgency.DAL.Interfaces;

public interface IImagesRepository : IBaseRepository<Image>
{
    Task<Image> GetByProperty(Guid Property_id);
}