using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStore.API.Extensions;
using ReStore.API.RequestHelpers;
using ReStore.Application.IRepositories;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.API.Controllers;

public class ProductsController : BaseController
{
        private readonly ReStoreContext _context;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IMapper _mapper;

        public ProductsController(ReStoreContext context, IProductReadRepository productReadRepository, IMapper mapper)
        {
                _context = context;
                _productReadRepository = productReadRepository;
                _mapper = mapper;
        }


        #region Ürünleri Getir

        [HttpGet]                       // Returns all products -- api/products
        public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery] ProductParams productParams)
        {

                var query = _context.Products
                            .Sort(productParams.OrderBy)
                            .Search(productParams.SearchTerm)
                            .Filter(productParams.Brands, productParams.Colors)
                            .AsQueryable();


                var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

                Response.AddPaginationHeader(products.MetaData);

                return products;
        }

        #endregion



        [HttpGet("{id}")]               // Pass parameter -- api/products/3
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
                var product = await _context.Products.FindAsync(id);

                if (product == null) return NotFound();         // throw not found error

                return product;
        }

        #region Filtreleme

        [HttpGet("filters")]
        public async Task<IActionResult> GetFilters()
        {
                var brands = await _context.Products.Select(p => p.Brand).Distinct().ToListAsync();
                var colors = await _context.Products.Select(p => p.Color.Name).Distinct().ToListAsync();

                return Ok(new { brands, colors });
        }

        #endregion
}
