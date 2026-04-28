using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services.Interfaces;

public interface IProjectService: IService<Project>
{
    public Task<Project> CreateProject(CreateProjectDto createProjectDto);
    public Task<Project> UpdateProject(Guid id, UpdateProjectDto updateProjectDto);
}