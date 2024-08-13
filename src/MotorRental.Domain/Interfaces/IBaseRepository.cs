namespace MotorRental.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, string? createdBy = null);
        Task<T?> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task UpdateAsync(T entity, string? modifiedBy = null);
        Task DeleteByIdAsync(int id);
        Task SaveAsync();
    }
}
