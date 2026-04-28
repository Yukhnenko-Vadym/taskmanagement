using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService =  userService;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        var user = await _userService.GetById(id);
        
        return Ok(user);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAllUsers()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
    
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(CreateUserDto createUserDto)
    {
        var user = await _userService.CreateUser(createUserDto);
        
        return Ok(user);
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<User>> UpdateUser(Guid id, UpdateUserDto updateUserDto)
    {
        var user = await _userService.UpdateUser(id, updateUserDto);
       
        return Ok(user);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var deleted = await _userService.Delete(id);

        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}