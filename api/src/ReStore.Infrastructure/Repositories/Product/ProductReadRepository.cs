using Microsoft.EntityFrameworkCore;
using ReStore.Application.IRepositories;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.Infrastructure.Repositories;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
        private readonly ReStoreContext _context;

        public ProductReadRepository(ReStoreContext context) : base(context)
        {
                _context = context;
        }

        public IQueryable<Product> GetAllProductsForIndex()
        {
                return _context.Products
                                .Include(x => x.Color)
                                .AsQueryable();
        }

        //public async  Task<IQueryable<Product>> GetAllProductsForIndex()
        //{
        //        return await  _context.Products
        //                        .Include(x => x.Color)
        //                        .ToListAsync();
        //}
}
