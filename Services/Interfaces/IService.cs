namespace TaskManagementApi.Services.Interfaces;

public interface IService<TResponse> 
    where TResponse: class
{
    public Task<List<TResponse>> GetAll();
    public Task<TResponse> GetById(Guid id);
    public Task<bool> Delete(Guid id);
}