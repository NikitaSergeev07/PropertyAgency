using PropertyAgency.DAL.Interfaces;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace Services.Implementations;

public class ImagesService: IImagesService
{
    private readonly IImagesRepository _transactionsRepository;

    public ImagesService(IImagesRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
    }
    
    public async Task<IEnumerable<Image>> GetImages()
    {
        return await _transactionsRepository.Get();
    }

    public async Task<Image> GetById(Guid id)
    {
        return await _transactionsRepository.GetById(id);
    }

    public async Task<bool> DeleteImage(Guid id)
    {
        return await _transactionsRepository.Delete(id);
    }

    public async Task<Image> CreateImage(Image entity)
    {
        return await _transactionsRepository.Create(entity);
    }

    public async Task<bool> UpdateImage(Image entity)
    {
        return await _transactionsRepository.Update(entity);
    }

    public async Task<Image> GetImageByPropertyId(Guid PropertyId)
    {
        return await _transactionsRepository.GetByProperty(PropertyId);
    }
}