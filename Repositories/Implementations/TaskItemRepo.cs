using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;

namespace TaskManagementApi.Repositories.Implementations;

public class TaskItemRepo: ITaskItemRepository
{
    private readonly TMContext _context;
    
    public TaskItemRepo(TMContext context) => _context = context;
    
    public async Task<TaskItem> Add(TaskItem entity)
    {
        await _context.TaskItems.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TaskItem> Update(TaskItem entity)
    {
        _context.TaskItems.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(TaskItem entity)
    {
        _context.TaskItems.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<TaskItem>> GetAllTaskItems()
    {
        return await _context.TaskItems
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskItemById(Guid searchId)
    {
        return await _context.TaskItems.FindAsync(searchId);
    }
}