using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStore.API.Services;
using ReStore.Application.DTOs;
using ReStore.Application.Extensions;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.API.Controllers
{
     public class BasketController : BaseController
     {
          private readonly ReStoreContext _context;
          private readonly UserManager<AppUser> _userManager;
          private readonly IUserService _userService;
          private readonly IHttpContextAccessor _accessor;

          public BasketController(ReStoreContext context, UserManager<AppUser> userManager, IUserService userService, IHttpContextAccessor accessor)
          {
               _context = context;
               _userManager = userManager;
               _userService = userService;
               _accessor = accessor;
          }

          [HttpGet(Name = "GetBasket")]
          public async Task<ActionResult<BasketDto>> GetBasket()
          {
               var basket = await RetrieveBasket(GetBuyerId());

               if (basket == null) return NotFound();

               return basket.MapBasketToDto();
          }

          #region Add and Remove

          // api/basket?productId=3&quantity=2
          [HttpPost]
          public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
          {
               // get basket || create basket
               //var basket = await RetrieveBasket(GetBuyerId());
               var basket = await RetrieveBasket(GetBuyerId());
               if (basket == null) basket = CreateBasket();

               // get product
               var product = await _context.Products.FindAsync(productId);
               if (product == null) return BadRequest(new ProblemDetails { Title = "Aradığınız ürün bulunamadı!" });

               // add item
               basket.AddItem(product, quantity);

               // save changes
               var result = await _context.SaveChangesAsync() > 0;
               if (result) return CreatedAtRoute("GetBasket", basket.MapBasketToDto());

               return BadRequest(new ProblemDetails { Title = "Ürünü sepete eklemede bir sorun yaşanıyor!" });
          }

          [HttpDelete]
          public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
          {
               // get basket
               var basket = await RetrieveBasket(GetBuyerId());
               if (basket == null) return NotFound();

               // remove basket
               basket.RemoveItem(productId, quantity);

               // save changes
               var result = await _context.SaveChangesAsync() > 0;
               if (result) return Ok();

               return BadRequest(new ProblemDetails { Title = "Ürünü sepetten çıkarmada bir sorun yaşanıyor!" });
          }

          #endregion


          #region Retrieve Basket

          private async Task<Basket> RetrieveBasket(string buyerId)
          {
               if (string.IsNullOrEmpty(buyerId))
               {
                    Response.Cookies.Delete("buyerId");
                    //Response.Cookies.Append("buyerId", "", new CookieOptions()
                    //{
                    //     Expires = DateTime.Now.AddDays(-1)
                    //});
                    return null;
               }

               return await _context.Baskets
                         .Include(i => i.Items)
                         .ThenInclude(p => p.Product)
                         .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
               //.FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]) ?? new Basket();
          }
          #endregion


          #region Create Basket if it doesn't exist

          private Basket CreateBasket()
          {
               var buyerId = User.Identity?.Name;

               if (string.IsNullOrEmpty(buyerId))
               {
                    buyerId = Guid.NewGuid().ToString();
                    var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                    Response.Cookies.Append("buyerId", buyerId, cookieOptions);
               }

               var basket = new Basket { BuyerId = buyerId };
               _context.Baskets.Add(basket);
               _context.SaveChanges();
               return basket;
          }

          #endregion


          #region Get UserName Or Cookies ["buyerId"]

          [Authorize]
          private string GetBuyerId()
          {
               // I can't access the UserName of the logged in user

               // return null
               var userNameTest1 = _userService.GetMyName();
               // return null
               var userNameTest2 = GetMe();
               // return null
               string userNameTest3 = _accessor.HttpContext.User.Identity.Name;
               //return null
               var user = _userManager.GetUserName(HttpContext.User);

               if (userNameTest1 == null)
                    return Request.Cookies["buyerId"];
               else
                    return userNameTest1;
          }

          #endregion


          #region Get UserName

          [Authorize]
          [HttpGet("GetUserName")]
          public ActionResult<string> GetMe()
          {
               // It's works and I can access the UserName of the logged in user
               var userName = _userService.GetMyName();

               return Ok(userName);
          }

          #endregion
     }
}
