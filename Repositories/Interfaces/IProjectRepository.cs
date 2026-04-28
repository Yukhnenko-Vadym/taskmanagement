using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories.Interfaces;

public interface IProjectRepository: IRepository<Project>
{
    public Task<List<Project>> GetAllProjects();
    public Task<Project> GetProjectById(Guid searchId);
}