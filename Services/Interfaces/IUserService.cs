using TaskManagementApi.DTOs.Request;
using TaskManagementApi.DTOs.Response;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services.Interfaces;

public interface IUserService: IService<UserResponseDto>
{
    public Task<UserResponseDto> CreateUser(CreateUserDto createTaskItemDto);
    public Task<UserResponseDto> UpdateUser(Guid id, UpdateUserDto updateTaskItemDto);
    public Task<User> Login(LoginUserDto loginUserDto);
    public string GenerateHash(string password, string salt);
    public string GenerateSalt();
}