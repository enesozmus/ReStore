using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStore.Application.Extensions;
using ReStore.Application.RequestHelpers;
using ReStore.Application.IRepositories;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;
using Microsoft.AspNetCore.Authorization;
using ReStore.Application.DTOs;
using ReStore.API.Services;

namespace ReStore.API.Controllers;

public class ProductsController : BaseController
{
     private readonly ReStoreContext _context;
     private readonly IProductReadRepository _productReadRepository;
     private readonly IMapper _mapper;
     private readonly ImageService _imageService;

     public ProductsController(ReStoreContext context, IProductReadRepository productReadRepository, IMapper mapper, ImageService imageService)
     {
          _context = context;
          _productReadRepository = productReadRepository;
          _mapper = mapper;
          _imageService = imageService;
     }


     #region Ürünleri Getir

     [HttpGet]                       // Returns all products -- api/products
     public async Task<ActionResult<PagedList<Product>>> GetProducts([FromQuery] ProductParams productParams)
     {

          var query = _context.Products
                      .Sort(productParams.OrderBy)
                      .Search(productParams.SearchTerm)
                      .Filter(productParams.Brands, productParams.Colors, productParams.Categories)
                      .AsQueryable();


          var products = await PagedList<Product>.ToPagedList(query, productParams.PageNumber, productParams.PageSize);

          Response.AddPaginationHeader(products.MetaData);

          return products;
     }

     #endregion


     [HttpGet("{id}", Name = "GetProduct")]               // Pass parameter -- api/products/3
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
          var colors = await _context.Products.Select(p => p.Color).Distinct().ToListAsync();
          var categories = await _context.Products.Select(p => p.Category).Distinct().ToListAsync();

          return Ok(new { brands, colors, categories });
     }

     #endregion


     #region Ürün Ekle

     [Authorize]
     [HttpPost]
     public async Task<ActionResult> CreateProduct([FromForm] CreateProductDto productDto)
     {
          // automapper
          var product = _mapper.Map<Product>(productDto);

          if (productDto.File != null)
          {
               var imageResult = await _imageService.AddImageAsync(productDto.File);

               if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

               product.PictureUrl = imageResult.SecureUrl.ToString();
               product.PublicId = imageResult.PublicId;
          }

          _context.Products.Add(product);

          var result = await _context.SaveChangesAsync() > 0;

          if (result) return CreatedAtRoute("GetProduct", new { Id = product.Id }, product);

          return BadRequest(new ProblemDetails { Title = "Problem creating new product" });
     }

     #endregion

     #region Ürün Güncelle

     [Authorize(Roles = "Admin")]
     [HttpPut]
     public async Task<ActionResult> UpdateProduct([FromForm] UpdateProductDto productDto)
     {
          var product = await _context.Products.FindAsync(productDto.Id);

          if (product == null) return NotFound();

          // automapper
          _mapper.Map(productDto, product);

          if (productDto.File != null)
          {
               var imageResult = await _imageService.AddImageAsync(productDto.File);

               if (imageResult.Error != null)
                    return BadRequest(new ProblemDetails { Title = imageResult.Error.Message });

               if (!string.IsNullOrEmpty(product.PublicId))
                    await _imageService.DeleteImageAsync(product.PublicId);

               product.PictureUrl = imageResult.SecureUrl.ToString();
               product.PublicId = imageResult.PublicId;
          }

          var result = await _context.SaveChangesAsync() > 0;

          if (result) return Ok(product);

          return BadRequest(new ProblemDetails { Title = "Problem updating product" });
     }

     #endregion

     #region Ürün Sil

     [Authorize(Roles = "Admin")]
     [HttpDelete("{id}")]
     public async Task<ActionResult> DeleteProduct(int id)
     {
          var product = await _context.Products.FindAsync(id);

          if (product == null) return NotFound();

          if (!string.IsNullOrEmpty(product.PublicId))
               await _imageService.DeleteImageAsync(product.PublicId);

          _context.Products.Remove(product);

          var result = await _context.SaveChangesAsync() > 0;

          if (result) return Ok();

          return BadRequest(new ProblemDetails { Title = "Problem deleting product" });
     }

     #endregion
}
