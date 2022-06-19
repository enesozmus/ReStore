using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.API.Controllers;

public class ProductsController : BaseController
{
        private readonly ReStoreContext _context;

        public ProductsController(ReStoreContext context)
        {
                _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts() => Ok(await _context.Products.ToListAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
                var product = await _context.Products.FindAsync(id);

                if (product == null) return NotFound();

                return product;
        }
}
