using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos;
using PropertyAgency.Domain.Entities;
using Services.Helpers;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers;


[Authorize(Policy = "UserPolicy")]
[ApiController]
[Route("[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly IUsersService _usersService;
    private readonly IFavoritesService _favoritesService;

    public FavoritesController(JwtService jwtService, IUsersService usersService, IFavoritesService favoritesService)
    {
        _jwtService = jwtService;
        _usersService = usersService;
        _favoritesService = favoritesService;
    }
    
    private Favorite MapCustomerObject(FavoriteDto payload, Guid userId)
    {
        var result = new Favorite();
        result.UserId = userId;
        result.PropertyId = payload.PropertyId;
        return result;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUserFavorites()
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }

        JwtSecurityToken jwtToken;
        try
        {
            jwtToken = _jwtService.Verify(token);
        }
        catch
        {
            return Unauthorized();
        }

        if (jwtToken == null)
        {
            return Unauthorized();
        }

        Guid userId = Guid.Parse(jwtToken.Subject);
        var user = await _usersService.GetById(userId);
        if (user == null)
        {
            return Unauthorized();
        }

        var favorites = await _favoritesService.GetFavoritesByUserId(userId);
        return Ok(favorites);
    }

    
    [HttpPost]
    public async Task<IActionResult> CreateFavorite(FavoriteDto favoriteDto)
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }

        JwtSecurityToken jwtToken;
        try
        {
            jwtToken = _jwtService.Verify(token);
        }
        catch
        {
            return Unauthorized();
        }

        if (jwtToken == null)
        {
            return Unauthorized();
        }

        Guid userId = Guid.Parse(jwtToken.Subject);
        var user = await _usersService.GetById(userId);
        if (user == null)
        {
            return Unauthorized();
        }

        var newFavorite = MapCustomerObject(favoriteDto, userId);
        var favoriteId = await _favoritesService.CreateFavorite(newFavorite);
        return Created($"/favorites/{favoriteId}", favoriteId);
    }

    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteFavorite(Guid id)
    {
        var token = Request.Cookies["jwt"];
        if (string.IsNullOrEmpty(token))
        {
            return Unauthorized();
        }

        JwtSecurityToken jwtToken;
        try
        {
            jwtToken = _jwtService.Verify(token);
        }
        catch
        {
            return Unauthorized();
        }

        if (jwtToken == null)
        {
            return Unauthorized();
        }

        Guid userId = Guid.Parse(jwtToken.Subject);
        var user = await _usersService.GetById(userId);
        if (user == null)
        {
            return Unauthorized();
        }

        var result = await _favoritesService.DeleteFavorite(id);
        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }

    
    
}