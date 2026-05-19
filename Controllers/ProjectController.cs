using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.DTOs.Response;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProjectController:ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult<ProjectResponseDto>> GetProjectById(Guid id)
    {
        var project = await _projectService.GetById(id);
        return Ok(project);
    }

    [HttpGet]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult<List<ProjectResponseDto>>> GetAllProjects()
    {
        var projects = await _projectService.GetAll();
        
        return Ok(projects);
    }

    [HttpPost]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult<ProjectResponseDto>> CreateProject(CreateProjectDto createProjectDto)
    {
        var project = await _projectService.CreateProject(createProjectDto);
        
        return Ok(project);
    }

    [HttpPatch("{id:guid}")]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult<ProjectResponseDto>> UpdateProject(Guid id, UpdateProjectDto updateProjectDto)
    {
        var project = await _projectService.UpdateProject(id, updateProjectDto);
        
        return Ok(project);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "TeamLead")]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        var deleted = await _projectService.Delete(id);
        
        if (!deleted)
            return NotFound();
        
        return NoContent();
    }
}