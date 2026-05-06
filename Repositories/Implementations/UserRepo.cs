using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;

namespace TaskManagementApi.Repositories.Implementations;

public class UserRepo: IUserRepository
{
    private readonly TMContext _context;

    public UserRepo(TMContext context)
    {
        _context = context;
    }
    
    public async Task<User> Add(User entity)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task Delete(User entity)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetUserById(Guid searchId)
    {
        return await _context.Users.FindAsync(searchId);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByIdWithProjects(Guid searchId)
    {
        return await _context.Users
            .Include(u => u.OwnedProjects)
            .Include(u => u.ProjectUsers)
            .ThenInclude(pu => pu.Project)
            .FirstOrDefaultAsync(u => u.Id == searchId);
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}