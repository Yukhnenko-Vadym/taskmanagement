using TaskManagementApi.Models;

namespace TaskManagementApi.Repositories.Interfaces;

public interface IUserRepository: IRepository<User>
{
    public Task<List<User>> GetAllUsers();
    public Task<User> GetUserById(Guid searchId);
}