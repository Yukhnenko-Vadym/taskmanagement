namespace TaskManagementApi.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<T> Add(T entity);
    public Task SaveChanges();
    public Task Delete(T entity);
}