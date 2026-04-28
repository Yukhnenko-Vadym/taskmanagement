using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemController:ControllerBase
{
    private readonly ITaskItemService _taskItemService;

    public TaskItemController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskItem>> GetTaskItemById(Guid id)
    {
        var taskItem = await _taskItemService.GetById(id);
        
        return Ok(taskItem);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAllTaskItems()
    {
        var taskItems = await _taskItemService.GetAll();
        
        return Ok(taskItems);
    }
    
    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTaskItem(CreateTaskItemDto createTaskItemDto)
    {
        var taskItem = await _taskItemService.CreateTaskItem(createTaskItemDto);
        
        return Ok(taskItem);
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<TaskItem>> UpdateTaskItem(Guid id, UpdateTaskItemDto updateTaskItemDto)
    {
        var taskItem = await _taskItemService.UpdateTaskItem(id, updateTaskItemDto);
       
        return Ok(taskItem);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var deleted = await _taskItemService.Delete(id);

        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}