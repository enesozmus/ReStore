using ReStore.Application.IRepositories;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.Infrastructure.Repositories;

public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
{
     public BasketWriteRepository(ReStoreContext context) : base(context) { }
}
