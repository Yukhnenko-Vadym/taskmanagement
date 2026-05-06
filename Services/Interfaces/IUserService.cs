using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services.Interfaces;

public interface IUserService: IService<User>
{
    public Task<User> CreateUser(CreateUserDto createTaskItemDto);
    public Task<User> UpdateUser(Guid id, UpdateUserDto updateTaskItemDto);
    public Task<User> Login(LoginUserDto loginUserDto);
    public string GenerateHash(string password, string salt);
    public string GenerateSalt();
}