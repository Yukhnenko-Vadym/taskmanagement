namespace TaskManagementApi.Services.Interfaces;

public interface IService<T> where T: class
{
    public Task<List<T>> GetAll();
    public Task<T> GetById(Guid id);
    public Task<bool> Delete(Guid id);
}