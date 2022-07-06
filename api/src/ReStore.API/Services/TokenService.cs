using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ReStore.Application.DTOs;
using ReStore.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReStore.API.Services;

public class TokenService
{
     private readonly UserManager<AppUser> _userManager;
     private readonly IConfiguration _config;
     private readonly IMapper _mapper;

     public TokenService(UserManager<AppUser> userManager, IConfiguration config, IMapper mapper)
     {
          _userManager = userManager;
          _config = config;
          _mapper = mapper;
     }

     public async Task<string> GenerateToken(AppUser user)
     {
          var claims = new List<Claim>
          {
               new Claim(ClaimTypes.Email, user.Email),
               new Claim(ClaimTypes.Name, user.UserName)
          };

          var roles = await _userManager.GetRolesAsync(user);

          foreach (var role in roles)
          {
               claims.Add(new Claim(ClaimTypes.Role, role));
          }

          var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:TokenKey"]));
          var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

          var tokenOptions = new JwtSecurityToken
          (
              issuer: null,
              audience: null,
              claims: claims,
              expires: DateTime.Now.AddDays(7),
              signingCredentials: creds
          );

          return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
     }
}
