using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReStore.API.Services;
using ReStore.Application.DTOs;
using ReStore.Application.Extensions;
using ReStore.Domain.Entities;
using ReStore.Infrastructure.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReStore.API.Controllers;

public class AccountController : BaseController
{
     private readonly ReStoreContext _context;
     private readonly UserManager<AppUser> _userManager;
     private readonly IUserService _userService;
     private readonly IConfiguration _configuration;
     private readonly IMapper _mapper;

     public AccountController(ReStoreContext context, UserManager<AppUser> userManager, IMapper mapper, IUserService userService, IConfiguration configuration)
     {
          _context = context;
          _userManager = userManager;
          _mapper = mapper;
          _userService = userService;
          _configuration = configuration;
     }

     #region Test

     [HttpGet, Authorize]
     public ActionResult<string> GetMe()
     {
          var userName = _userService.GetMyName();
          return Ok(userName);
     }

     #endregion


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
               Token = CreateToken(userModel),
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


     #region Giriş Yapmış Mevcut Kullanıcıyı Getir

     //[Authorize(AuthenticationSchemes = "Bearer")]
     //[HttpGet("currentUser")]
     //public async Task<IActionResult> GetCurrentUser() => Ok(await _mediator.Send(new CurrentUserQueryRequest()));

     //[Authorize(AuthenticationSchemes = "Bearer")]
     [Authorize]
     [HttpGet("currentUser")]
     public async Task<ActionResult<CurrentUserQueryResponse>> GetCurrentUser()
     {
          // Direkt giriş yapmış kullanıcıyı getirir.
          var user = _userManager.Users.FirstOrDefault(x => x.Email == User.Identity.Name);

          var userBasket = await RetrieveBasket(user.UserName);


          var userModel = _mapper.Map<LoginCommandResponse>(user);

          return new CurrentUserQueryResponse
          {
               Id = userModel.Id,
               FirstName = userModel.FirstName,
               LastName = userModel.LastName,
               UserName = userModel.UserName,
               Email = userModel.Email,
               Token = CreateToken(userModel),
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

     #region Token

     private string CreateToken(LoginCommandResponse response)
     {
          List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, response.UserName),
                new Claim(ClaimTypes.Role, "Admin")
            };

          var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
              _configuration.GetSection("AppSettings:Token").Value));

          var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

          var token = new JwtSecurityToken(
              claims: claims,
              expires: DateTime.Now.AddDays(1),
              signingCredentials: creds);

          var jwt = new JwtSecurityTokenHandler().WriteToken(token);

          return jwt;
     }

     #endregion
}
