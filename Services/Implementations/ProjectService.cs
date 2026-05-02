using AutoMapper;
using TaskManagementApi.DTOs.Request;
using TaskManagementApi.Models;
using TaskManagementApi.Repositories.Interfaces;
using TaskManagementApi.Services.Interfaces;

namespace TaskManagementApi.Services.Implementations;

public class ProjectService: IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, IMapper mapper, IUserRepository userRepository)
    {
        _projectRepository = projectRepository;
        _mapper = mapper;
        _userRepository = userRepository;
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
        var owner = await _userRepository.GetUserById(createProjectDto.OwnerId);

        if (owner is null)
            throw new KeyNotFoundException($"User with id {createProjectDto.OwnerId} not found");

        var project = _mapper.Map<Project>(createProjectDto);
        project.StartedAt = DateTime.UtcNow;

        return await _projectRepository.Add(project);
    }

    public async Task<Project> UpdateProject(Guid id, UpdateProjectDto updateProjectDto)
    {
        var project = await _projectRepository.GetProjectById(id);
        
        if (project == null)
            throw new KeyNotFoundException($"Project {id} was not found.");

        _mapper.Map(updateProjectDto, project);
        
        await _projectRepository.SaveChanges();
        return project;
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