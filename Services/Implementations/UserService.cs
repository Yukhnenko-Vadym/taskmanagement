using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private IUserService _userServiceImplementation;

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

    public async Task<User> CreateUser(CreateUserDto createUserDto)
    {
        var user = _mapper.Map<User>(createUserDto);

        string salt = GenerateSalt();
        string hash = GenerateHash(createUserDto.Password, salt);

        user.PasswordSalt = salt;
        user.PasswordHash = hash;

        return await _userRepository.Add(user);
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

    public async Task<User?> Login(LoginUserDto loginUserDto)
    {
        var user = await _userRepository.GetByEmail(loginUserDto.Email);
        
        if (user is null)
            return null;
        
        var computedHash = GenerateHash(loginUserDto.Password, user.PasswordSalt);
        
        if (computedHash != user.PasswordHash)
            return null;

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

    public string GenerateHash(string password, string saltBase64)
    {
        var saltBytes = Convert.FromBase64String(saltBase64);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            saltBytes,
            35000,
            HashAlgorithmName.SHA512,
            64);

        return Convert.ToBase64String(hash);
    }

    public string GenerateSalt()
    {
        var saltBytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(saltBytes);
    }
}