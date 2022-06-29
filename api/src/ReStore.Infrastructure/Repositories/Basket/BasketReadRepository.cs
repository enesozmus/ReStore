using ReStore.Application.IRepositories;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.Infrastructure.Repositories;

public class BasketReadRepository : ReadRepository<Basket>, IBasketReadRepository
{
     public BasketReadRepository(ReStoreContext context) : base(context) { }
}