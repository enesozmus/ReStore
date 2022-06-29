using Microsoft.IdentityModel.Tokens;
using ReStore.Application.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReStore.Application.Features.AuthenticationOperations;

public class JwtTokenGenerator
{
        public static string GenerateToken(LoginCommandResponse login)
        {
                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, login.Id.ToString()),
                        new Claim(ClaimTypes.Name, login.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Enesenesenes1.LC"));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expireDate = DateTime.Now.AddDays(2);

                var token = new JwtSecurityToken(
                        issuer: "http://localhost",
                        audience: "http://localhost",
                        claims,
                        notBefore: DateTime.Now,
                        expires: expireDate,
                        signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
        }
}
