namespace TaskManagementApi.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<T> Add(T entity);
    public Task<T> Update(T entity);
    public Task Delete(T entity);
}