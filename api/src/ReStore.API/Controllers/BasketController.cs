using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStore.Application.DTOs;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.API.Controllers
{
        public class BasketController : BaseController
        {
                private readonly ReStoreContext _context;
                public BasketController(ReStoreContext context)
                {
                        _context = context;
                }

                [HttpGet(Name = "GetBasket")]
                public async Task<ActionResult<BasketDto>> GetBasket()
                {
                        var basket = await RetrieveBasket();

                        if (basket.BuyerId == null) return NotFound();

                        return MapBasketToDto(basket);
                }

                // api/basket?productId=3&quantity=2
                [HttpPost]
                public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
                {
                        // get basket || create basket
                        var basket = await RetrieveBasket();
                        if (basket.Items.Count == 0) basket = CreateBasket();

                        // get product
                        var product = await _context.Products.FindAsync(productId);
                        if (product == null) return BadRequest(new ProblemDetails { Title = "Aradığınız ürün bulunamadı!" });

                        // add item
                        basket.AddItem(product, quantity);

                        // save changes
                        var result = await _context.SaveChangesAsync() > 0;
                        if (result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket));

                        return BadRequest(new ProblemDetails { Title = "Ürünü sepete eklemede bir sorun yaşanıyor!" });
                }

                [HttpDelete]
                public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
                {
                        // get basket
                        var basket = await RetrieveBasket();
                        if (basket == null) return NotFound();

                        // remove basket
                        basket.RemoveItem(productId, quantity);

                        // save changes
                        var result = await _context.SaveChangesAsync() > 0;
                        if (result) return Ok();

                        return BadRequest(new ProblemDetails { Title = "Ürünü sepetten çıkarmada bir sorun yaşanıyor!" });
                }




                #region MyRegion

                private async Task<Basket> RetrieveBasket()
                {
                        return await _context.Baskets
                            .Include(i => i.Items)
                            .ThenInclude(p => p.Product)
                            //.FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
                            .FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]) ?? new Basket();
                }

                private Basket CreateBasket()
                {
                        var buyerId = Guid.NewGuid().ToString();
                        var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                        Response.Cookies.Append("buyerId", buyerId, cookieOptions);
                        var basket = new Basket { BuyerId = buyerId };
                        _context.Baskets.Add(basket);
                        return basket;
                }

                private BasketDto MapBasketToDto(Basket basket)
                {
                        return new BasketDto
                        {
                                Id = basket.Id,
                                BuyerId = basket.BuyerId,
                                Items = basket.Items.Select(item => new BasketItemDto
                                {
                                        ProductId = item.ProductId,
                                        Name = item.Product.Name,
                                        Price = item.Product.Price,
                                        PictureUrl = item.Product.PictureUrl,
                                        Brand = item.Product.Brand,
                                        Quantity = item.Quantity
                                }).ToList()
                        };
                }

                #endregion
        }
}
