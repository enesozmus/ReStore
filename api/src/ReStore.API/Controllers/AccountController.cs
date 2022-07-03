using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStore.API.Services;
using ReStore.Application.DTOs;
using ReStore.Application.Extensions;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;

namespace ReStore.API.Controllers;

public class AccountController : BaseController
{
     private readonly ReStoreContext _context;
     private readonly UserManager<AppUser> _userManager;
     private readonly IUserService _userService;
     private readonly TokenService _tokenService;
     private readonly IConfiguration _configuration;
     private readonly IMapper _mapper;

     public AccountController(ReStoreContext context, UserManager<AppUser> userManager, IMapper mapper, IUserService userService, IConfiguration configuration, TokenService tokenService)
     {
          _context = context;
          _userManager = userManager;
          _mapper = mapper;
          _userService = userService;
          _configuration = configuration;
          _tokenService = tokenService;
     }


     #region Giriş Yap

     [HttpPost("login")]
     public async Task<ActionResult<LoginCommandResponse>> Login(LoginCommandRequest request)
     {
          var user = await _userManager.FindByNameAsync(request.UserName);

          if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
               return Unauthorized();

          // basket
          var userBasket = await RetrieveBasket(request.UserName);
          var anonBasket = await RetrieveBasket(Request.Cookies["buyerId"]);

          if (anonBasket != null)
          {
               if (userBasket != null) _context.Baskets.Remove(userBasket);
               anonBasket.BuyerId = user.UserName;
               Response.Cookies.Delete("buyerId");
               await _context.SaveChangesAsync();
          }
          var userModel = _mapper.Map<LoginCommandResponse>(user);

          return new LoginCommandResponse
          {
               Id = userModel.Id,
               FirstName = userModel.FirstName,
               LastName = userModel.LastName,
               Email = userModel.Email,
               UserName = userModel.UserName,
               Token = await _tokenService.GenerateToken(userModel),
               Basket = anonBasket != null ? anonBasket.MapBasketToDto() : userBasket?.MapBasketToDto()
          };
     }

     #endregion


     #region Kayıt Ol

     [HttpPost("register")]
     public async Task<ActionResult> Register(RegisterDto registerDto)
     {
          // mapping
          var user = new AppUser { FirstName = registerDto.FirstName, LastName = registerDto.LastName, UserName = registerDto.Username, Email = registerDto.Email };

          // oluştur
          var result = await _userManager.CreateAsync(user, registerDto.Password);

          // kontrol
          if (!result.Succeeded)
          {
               foreach (var error in result.Errors)
               {
                    ModelState.AddModelError(error.Code, error.Description);
               }

               return ValidationProblem();
          }

          // rol
          await _userManager.AddToRoleAsync(user, "Member");

          return StatusCode(201);
     }
     #endregion


     #region Kayıtlı Adresi Getir

     [Authorize]
     [HttpGet("savedAddress")]
     public async Task<ActionResult<UserAddress>> GetSavedAddress()
     {
          return await _userManager.Users
              .Where(x => x.UserName == User.Identity.Name)
              .Select(user => user.Address)
              .FirstOrDefaultAsync();
     }

     #endregion


     #region Giriş Yapmış Mevcut Kullanıcıyı Getir

     //[Authorize(AuthenticationSchemes = "Bearer")]
     //[HttpGet("currentUser")]
     //public async Task<IActionResult> GetCurrentUser() => Ok(await _mediator.Send(new CurrentUserQueryRequest()));

     //[Authorize(AuthenticationSchemes = "Bearer")]
     [Authorize]
     [HttpGet("currentUser")]
     public async Task<ActionResult<CurrentUserQueryResponse>> GetCurrentUser()
     {
          var user = await _userManager.FindByNameAsync(User.Identity?.Name);

          var userBasket = await RetrieveBasket(user.UserName);

          var userModel = _mapper.Map<LoginCommandResponse>(user);

          return new CurrentUserQueryResponse
          {
               Id = userModel.Id,
               FirstName = userModel.FirstName,
               LastName = userModel.LastName,
               UserName = userModel.UserName,
               Email = userModel.Email,
               Token = await _tokenService.GenerateToken(userModel),
               Basket = userBasket?.MapBasketToDto()
          };
     }


     #endregion

     #region Retrive Basket

     private async Task<Basket> RetrieveBasket(string buyerId)
     {
          if (string.IsNullOrEmpty(buyerId))
          {
               Response.Cookies.Delete("buyerId");
               return null;
          }

          return await _context.Baskets
              .Include(i => i.Items)
              .ThenInclude(p => p.Product)
              .FirstOrDefaultAsync(x => x.BuyerId == buyerId);
          //.FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]) ?? new Basket();
     }

     #endregion
}
