using AutoMapper;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Services.Implementations;

public class UserService: IUserService
{
    private  readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<List<User>> GetAll()
    {
        return await _userRepository.GetAllUsers();
    }

    public async Task<User> GetById(Guid id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<User> CreateUser(CreateUserDto createTaskItemDto)
    {
        return await _userRepository.Add(_mapper.Map<User>(createTaskItemDto));
    }

    public async Task<User> UpdateUser(Guid id, UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetUserById(id);
        
        if (user == null)
            throw new KeyNotFoundException($"User with id {id} not found");
        
        _mapper.Map(updateUserDto, user);
        
        await _userRepository.SaveChanges();
        return user;
    }
    
    public async Task<bool> Delete(Guid id)
    {
        var user = await _userRepository.GetUserById(id);
        
        if (user is null)
            return false;
        
        await _userRepository.Delete(user);
        return true;
    }
}