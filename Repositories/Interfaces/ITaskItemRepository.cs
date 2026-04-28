using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories.Interfaces;

public interface ITaskItemRepository: IRepository<TaskItem>
{
    public Task<List<TaskItem>> GetAllTaskItems();
    public Task<TaskItem> GetTaskItemById(Guid searchId);
}