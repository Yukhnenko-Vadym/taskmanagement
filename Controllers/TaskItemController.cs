using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.DTOs.Response;
using TaskManagementApi.Models;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskItemController:ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<TaskItemResponseDto>> GetTaskItemById(Guid id)
    {
        var taskItem = await _taskItemService.GetById(id);
        
        return Ok(taskItem);
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<TaskItemResponseDto>>> GetAllTaskItems()
    {
        var taskItems = await _taskItemService.GetAll();
        
        return Ok(taskItems);
    }
    
    [HttpPost]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult<TaskItemResponseDto>> CreateTaskItem(CreateTaskItemDto createTaskItemDto)
    {
        var taskItem = await _taskItemService.CreateTaskItem(createTaskItemDto);
        
        return Ok(taskItem);
    }
    
    [HttpPatch("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<TaskItemResponseDto>> UpdateTaskItem(Guid id, UpdateTaskItemDto updateTaskItemDto)
    {
        var taskItem = await _taskItemService.UpdateTaskItem(id, updateTaskItemDto);
       
        return Ok(taskItem);
    }
    
    [HttpPatch("assign_task/{id:taskItemId}")]
    [Authorize]
    public async Task<ActionResult<TaskItem>> AssignTask(Guid taskItemId, Guid userId)
    {
        var taskItem = await _taskItemService.AssignTaskItem(taskItemId, userId);
       
        return Ok(taskItem);
    }
    
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleted = await _taskItemService.Delete(id);

        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}