using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services.Interfaces;

public interface ITaskItemService: IService<TaskItem>
{
    public Task<TaskItem> CreateTaskItem(CreateTaskItemDto createTaskItemDto);
    public Task<TaskItem> UpdateTaskItem(Guid id, UpdateTaskItemDto updateTaskItemDto);
}