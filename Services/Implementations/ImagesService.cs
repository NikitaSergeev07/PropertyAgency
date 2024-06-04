using Microsoft.AspNetCore.Http;
using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace Services.Implementations;

public class ImagesService: IImagesService
{
    private readonly IImagesRepository _imagesRepository;

    public ImagesService(IImagesRepository imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }
    
    public async Task<IEnumerable<Image>> GetImages()
    {
        return await _imagesRepository.Get();
    }

    public async Task<Image> GetById(Guid id)
    {
        return await _imagesRepository.GetById(id);
    }

    public async Task<bool> DeleteImage(Guid id)
    {
        return await _imagesRepository.Delete(id);
    }

    public async Task<string> SaveImage(IFormFile file, string uploadPath, Guid propertyId)
    {
        if (file == null || file.Length == 0)
            return null;

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var image = new Image
        {
            Id = Guid.NewGuid(),
            ImageUrl = filePath,
            PropertyId = propertyId
        };

        await _imagesRepository.Create(image);
        return filePath;
    }

    public async Task<bool> UpdateImage(Image entity)
    {
        return await _imagesRepository.Update(entity);
    }

    public async Task<List<Image>> GetByProperty(Guid PropertyId)
    {
        return await _imagesRepository.GetByProperty(PropertyId);
    }
}