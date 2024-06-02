using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers;


[ApiController]
[Route("[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImagesService _imagesService;
    public ImagesController(IImagesService transactionsService)
    {
        _imagesService = transactionsService;
    }
    
    private Image MapCustomerObject(ImageDto payload)
    {
        var result = new Image();
        result.ImageUrl= payload.ImageUrl;
        result.PropertyId = payload.PropertyId;
        return result;
    }
    [HttpGet]
    public async Task<IActionResult> GetImages()
    {
        var transactions = await _imagesService.GetImages();

        return Ok(transactions);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var transaction = await _imagesService.GetById(id);
        return Ok(transaction);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateImage(ImageDto transaction)
    {
        var newImage = MapCustomerObject(transaction);
        await _imagesService.CreateImage(newImage);
        return Created($"/Image/{newImage.Id}", newImage);
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateImage(Guid id, ImageDto transaction)
    {
        var updateImage = MapCustomerObject(transaction);
        updateImage.Id = id;
        await _imagesService.UpdateImage(updateImage);
        return Ok(updateImage);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        return Ok(await _imagesService.DeleteImage(id));
    }

    [HttpGet("by-PropertyId")]
    public async Task<IActionResult> GetImageByPropertyId([FromQuery] Guid PropertyId)
    {
        var transactions = await _imagesService.GetImageByPropertyId(PropertyId);
        return Ok(transactions);
    }
}