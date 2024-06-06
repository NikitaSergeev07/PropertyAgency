<<<<<<< HEAD
﻿using PropertyAgency.DAL.Interfaces;
=======
using Microsoft.AspNetCore.Http;
using PropertyAgency.DAL.Interfaces;
>>>>>>> Nikita
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace Services.Implementations;

public class ImagesService: IImagesService
{
<<<<<<< HEAD
    private readonly IImagesRepository _transactionsRepository;

    public ImagesService(IImagesRepository transactionsRepository)
    {
        _transactionsRepository = transactionsRepository;
=======
    private readonly IImagesRepository _imagesRepository;

    public ImagesService(IImagesRepository imagesRepository)
    {
        _imagesRepository = imagesRepository;
>>>>>>> Nikita
    }
    
    public async Task<IEnumerable<Image>> GetImages()
    {
<<<<<<< HEAD
        return await _transactionsRepository.Get();
=======
        return await _imagesRepository.Get();
>>>>>>> Nikita
    }

    public async Task<Image> GetById(Guid id)
    {
<<<<<<< HEAD
        return await _transactionsRepository.GetById(id);
=======
        return await _imagesRepository.GetById(id);
>>>>>>> Nikita
    }

    public async Task<bool> DeleteImage(Guid id)
    {
<<<<<<< HEAD
        return await _transactionsRepository.Delete(id);
    }

    public async Task<Image> CreateImage(Image entity)
    {
        return await _transactionsRepository.Create(entity);
=======
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
>>>>>>> Nikita
    }

    public async Task<bool> UpdateImage(Image entity)
    {
<<<<<<< HEAD
        return await _transactionsRepository.Update(entity);
    }

    public async Task<Image> GetImageByPropertyId(Guid PropertyId)
    {
        return await _transactionsRepository.GetByProperty(PropertyId);
=======
        return await _imagesRepository.Update(entity);
    }

    public async Task<List<Image>> GetByProperty(Guid PropertyId)
    {
        return await _imagesRepository.GetByProperty(PropertyId);
>>>>>>> Nikita
    }
}