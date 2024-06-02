using PropertyAgency.Domain.Entities;

namespace Services.Interfaces;

public interface IImagesService
{
    Task<IEnumerable<Image>> GetImages();

    Task<Image> GetById(Guid id);
    Task<bool> DeleteImage(Guid id);
    Task<Image> CreateImage(Image entity);
    Task<bool> UpdateImage(Image entity);

    Task<Image> GetImageByPropertyId(Guid ProperyId);

}