using AutoMapper;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Services.Implementations;

public class TaskItemService: ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IMapper _mapper;

    public TaskItemService(ITaskItemRepository taskItemRepository,  IMapper mapper)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
    }

    public async Task<List<TaskItem>> GetAll()
    {
        return await _taskItemRepository.GetAllTaskItems();
    }

    public async Task<TaskItem> GetById(Guid id)
    {
        return await _taskItemRepository.GetTaskItemById(id);
    }

   public async Task<TaskItem> CreateTaskItem(CreateTaskItemDto createTaskItemDto)
    {
        var newTaskItem = _mapper.Map<CreateTaskItemDto, TaskItem>(createTaskItemDto);
        await _taskItemRepository.Add(newTaskItem);
        return newTaskItem;
    }

    public async Task<TaskItem> UpdateTaskItem(Guid id, UpdateTaskItemDto updateTaskItemDto)
    {
       var taskItem = await _taskItemRepository.GetTaskItemById(id);
       
       if (taskItem is null)
           throw new ArgumentException($"Task item with {id} wasn't found");
       
       _mapper.Map(updateTaskItemDto, taskItem);
       
       await _taskItemRepository.SaveChanges();
       return taskItem;
    }
    
    public async Task<bool> Delete(Guid id)
    {
        var taskItem = await _taskItemRepository.GetTaskItemById(id);
       
        if (taskItem is null)
            return false;
        
        await _taskItemRepository.Delete(taskItem);
        return true;
    }
}