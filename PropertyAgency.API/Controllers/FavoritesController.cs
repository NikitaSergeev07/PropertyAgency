using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers;



[ApiController]
[Route("[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly IFavoritesService _favoritesService;
    public FavoritesController(IFavoritesService favoritesService)
    {
        _favoritesService = favoritesService;
    }
    
    private Favorite MapCustomerObject(FavoriteDto payload)
    {
        var result = new Favorite();
        result.UserId = payload.UserId;
        result.PropertyId = payload.PropertyId;
        return result;
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetFavorites()
    {
        var favorites = await _favoritesService.GetFavorites();
        
        return Ok(favorites);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var favorite = await _favoritesService.GetById(id);
        return Ok(favorite);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateFavorite(FavoriteDto favorite)
    {
        var newFavorite = MapCustomerObject(favorite);
        await _favoritesService.CreateFavorite(newFavorite);
        return Created($"/favorite/{newFavorite.Id}", newFavorite);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFavorite(Guid id)
    {
        return Ok(await _favoritesService.DeleteFavorite(id));
    
    }
    
    
}