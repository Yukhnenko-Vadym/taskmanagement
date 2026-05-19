using TaskManagementApi.DTOs.Request;
using TaskManagementApi.DTOs.Response;
using TaskManagementApi.Models;

namespace TaskManagementApi.Services.Interfaces;

public interface IProjectService: IService<ProjectResponseDto>
{
    public Task<ProjectResponseDto> CreateProject(CreateProjectDto createProjectDto);
    public Task<ProjectResponseDto> UpdateProject(Guid id, UpdateProjectDto updateProjectDto);
}