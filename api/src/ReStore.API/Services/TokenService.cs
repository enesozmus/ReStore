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

     public TokenService(UserManager<AppUser> userManager, IConfiguration config)
     {
          _userManager = userManager;
          _config = config;
     }

     public async Task<string> GenerateToken(LoginCommandResponse response)
     {
          var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, response.Email),
                new Claim(ClaimTypes.Name, response.UserName)
            };

          AppUser user = new();

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
