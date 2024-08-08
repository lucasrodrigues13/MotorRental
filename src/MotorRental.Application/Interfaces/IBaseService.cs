namespace MotorRental.Application.Interfaces
{
    public interface IBaseService<T> where T : class 
    {
        Task AddAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task UpdateAsync(T entity);
        Task DeleteByIdAsync(int id);
        Task SaveAsync();
    }
}
