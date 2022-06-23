using ReStore.Application.IRepositories;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.Infrastructure.Repositories;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
        public ProductWriteRepository(ReStoreContext context) : base(context) { }
}
