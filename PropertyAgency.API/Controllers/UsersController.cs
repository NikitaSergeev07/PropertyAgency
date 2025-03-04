using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyAgency.API.Dtos;
using PropertyAgency.Domain.Entities;
using Services.Interfaces;

namespace PropertyAgency.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;
    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    private User MapCustomerObject(UserDto payload)
    {
        var result = new User();
        result.UserName = payload.UserName;
        result.Email = payload.Email;
        result.Password = payload.Password;
        result.Role = payload.Role;
        return result;
    }
    
    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> GetUsers()
    {
        // var token = Request.Cookies["jwt"];
        // if (string.IsNullOrEmpty(token))
        // {
        //     return Unauthorized();
        // }
        var users = await _usersService.GetUsers();
        
        return Ok(users);
    }
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _usersService.GetById(id);
        return Ok(user);
    }
    
    [HttpGet("by-email/{{email}}")]
    public async Task<IActionResult> GetByEmail(string email)
    {
        var user = await _usersService.GetByEmail(email);
        return Ok(user);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto user)
    {
        var newUser = MapCustomerObject(user);
        await _usersService.CreateUser(newUser);
        return Created($"/user/{newUser.Id}", newUser);
    }
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateUser(Guid id, UserDto user)
    {
        var updateUser = MapCustomerObject(user);
        updateUser.Id = id;
        await _usersService.UpdateUser(updateUser);
        return Ok(updateUser);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        return Ok(await _usersService.DeleteUser(id));
    
    }
    
    
}