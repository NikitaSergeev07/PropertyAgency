using Microsoft.AspNetCore.Http;
using PropertyAgency.Domain.Entities;

namespace Services.Interfaces;

public interface IImagesService
{
    Task<IEnumerable<Image>> GetImages();

    Task<Image> GetById(Guid id);
    Task<bool> DeleteImage(Guid id);
    Task<string> SaveImage(IFormFile file, string uploadPath, Guid propertyId);
    Task<bool> UpdateImage(Image entity);
    Task<List<Image>> GetByProperty(Guid Property_id);
}