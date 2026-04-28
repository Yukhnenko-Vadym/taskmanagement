using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectController:ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Project>> GetProjectById(Guid id)
    {
        var project = await _projectService.GetById(id);
        return Ok(project);
    }

    [HttpGet]
    public async Task<ActionResult<List<Project>>> GetAllProjects()
    {
        var projects = await _projectService.GetAll();
        
        return Ok(projects);
    }

    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject(CreateProjectDto createProjectDto)
    {
        var project = await _projectService.CreateProject(createProjectDto);
        
        return Ok(project);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<Project>> UpdateProject(Guid id, UpdateProjectDto updateProjectDto)
    {
        var project = await _projectService.UpdateProject(id, updateProjectDto);
        
        return Ok(project);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        var deleted = await _projectService.Delete(id);
        
        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}