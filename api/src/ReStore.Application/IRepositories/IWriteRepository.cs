using ReStore.Domain.Entities;

namespace ReStore.Application.IRepositories;

public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
{
        #region Insert

        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        #endregion

        #region Update

        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);

        #endregion

        #region Delete

        //Task<bool> RemoveAsync(int id);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);

        #endregion

        #region SaveAsync

        Task<int> SaveAsync();

        #endregion

}
