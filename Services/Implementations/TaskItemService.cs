using AutoMapper;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.DTOs.Response;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Services.Implementations;

public class TaskItemService: ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public TaskItemService(ITaskItemRepository taskItemRepository,  IMapper mapper, IUserRepository userRepository)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<List<TaskItemResponseDto>> GetAll()
    {
        var taskItems = await _taskItemRepository.GetAllTaskItems();
        return _mapper.Map<List<TaskItemResponseDto>>(taskItems);
    }

    public async Task<TaskItemResponseDto> GetById(Guid id)
    {
        var taskItem = await _taskItemRepository.GetTaskItemById(id);
        return _mapper.Map<TaskItemResponseDto>(taskItem);
    }

   public async Task<TaskItemResponseDto> CreateTaskItem(CreateTaskItemDto createTaskItemDto)
    {
        var newTaskItem = _mapper.Map<CreateTaskItemDto, TaskItem>(createTaskItemDto);
        await _taskItemRepository.Add(newTaskItem);
        return _mapper.Map<TaskItemResponseDto>(newTaskItem);
    }

    public async Task<TaskItemResponseDto> UpdateTaskItem(Guid id, UpdateTaskItemDto updateTaskItemDto)
    {
       var taskItem = await _taskItemRepository.GetTaskItemById(id);
       
       if (taskItem is null)
           throw new ArgumentException($"Task item with {id} wasn't found");
       
       _mapper.Map(updateTaskItemDto, taskItem);
       
       await _taskItemRepository.SaveChanges();
       return _mapper.Map<TaskItemResponseDto>(taskItem);
    }
    
    public async Task<TaskItemResponseDto> AssignTaskItem(Guid taskItemId, Guid userId)
    {
        if (! await _userRepository.IsExist(userId))
            throw new  ArgumentException($"User with id {userId} was not found");
        
        var taskItem = await GetById(taskItemId);
        taskItem.AssigneeId = userId;
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