using ReStore.Domain.Entities;

namespace ReStore.Application.IRepositories;

public interface IProductReadRepository : IReadRepository<Product>
{
        IQueryable<Product> GetAllProductsForIndex();
}
