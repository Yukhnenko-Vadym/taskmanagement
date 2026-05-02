using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;

namespace TaskManagementApi.Repositories.Implementations;

public class ProjectRepo: IProjectRepository
{
    private readonly TMContext _context;

    public ProjectRepo(TMContext context)
    {
        _context = context;
    }
    
    public async Task<Project> Add(Project entity)
    {
        await _context.Projects.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(Project entity)
    {
        _context.Projects.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Project>> GetAllProjects()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<Project> GetProjectById(Guid searchId)
    {
        return await _context.Projects.FindAsync(searchId);
    }
    
    public async Task<Project?> GetProjectByIdWithDetails(Guid searchId)
    {
        return await _context.Projects
            .Include(p => p.Owner)
            .Include(p => p.Tasks)
            .Include(p => p.ProjectUsers)
            .ThenInclude(pu => pu.User)
            .FirstOrDefaultAsync(p => p.Id == searchId);
    }
    
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}