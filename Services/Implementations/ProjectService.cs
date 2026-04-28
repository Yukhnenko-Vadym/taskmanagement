using AutoMapper;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Services.Implementations;

public class ProjectService: IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
    }
    
    public async Task<List<Project>> GetAll()
    {
        return await _projectRepository.GetAllProjects();
    }

    public async Task<Project> GetById(Guid id)
    {
        return await _projectRepository.GetProjectById(id);
    }
    
    public async Task<Project> CreateProject(CreateProjectDto createProjectDto)
    {
        return await _projectRepository.Add(_mapper.Map<Project>(createProjectDto));
    }

    public async Task<Project> UpdateProject(Guid id, UpdateProjectDto updateProjectDto)
    {
        var project = await _projectRepository.GetProjectById(id);
        
        if (project == null)
            throw new KeyNotFoundException($"Project {id} was not found.");
        
        return await _projectRepository.Update(_mapper.Map<Project>(updateProjectDto));
    }
    
    public async Task<bool> Delete(Guid id)
    {
        var project = await _projectRepository.GetProjectById(id);

        if (project is null)
            return false;

        await _projectRepository.Delete(project);
        return true;
    }
}