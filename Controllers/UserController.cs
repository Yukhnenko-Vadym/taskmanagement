using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.Data;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.DTOs.Response;
using TaskManagementApi.Models;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService, TMContext context)
    {
        _userService =  userService;
    }
    
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult<UserResponseDto>> GetUserById(Guid id)
    {
        var user = await _userService.GetById(id);
        
        return Ok(user);
    }
    
    [HttpGet]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult<List<UserResponseDto>>> GetAllUsers()
    {
        var users = await _userService.GetAll();
        return Ok(users);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<object>> Login([FromBody] LoginUserDto loginUserDto,
        [FromServices] ITokenService tokenService)
    {
        var user = await _userService.Login(loginUserDto);
        
        if (user is null)
            return Unauthorized("Invalid email or password");
        
        var token = tokenService.GenerateToken(user);
        
        return Ok(new { token });
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserResponseDto>> RegisterAsync(CreateUserDto createUserDto)
    {
        var user = await _userService.CreateUser(createUserDto);
        
        return Ok(user);
    }

    [HttpPatch("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<UserResponseDto>> UpdateUser(Guid id, UpdateUserDto updateUserDto)
    {
        var user = await _userService.UpdateUser(id, updateUserDto);
       
        return Ok(user);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        var deleted = await _userService.Delete(id);

        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}