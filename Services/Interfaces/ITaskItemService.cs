using TaskManagementApi.DTOs.Request;
using TaskManagementApi.DTOs.Response;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services.Interfaces;

public interface ITaskItemService: IService<TaskItemResponseDto>
{
    public Task<TaskItemResponseDto> CreateTaskItem(CreateTaskItemDto createTaskItemDto);
    public Task<TaskItemResponseDto> UpdateTaskItem(Guid id, UpdateTaskItemDto updateTaskItemDto);
    Task<TaskItemResponseDto> AssignTaskItem(Guid taskItemId, Guid userId);
}