using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReStore.Domain.Entities;
using System.Security.Claims;

namespace ReStore.API.Controllers
{
     public class TestsContorller : BaseController
     {
          private readonly UserManager<AppUser> _userManager;
          private readonly IHttpContextAccessor? _accessor;

          public TestsContorller(UserManager<AppUser> userManager, IHttpContextAccessor? accessor)
          {
               _userManager = userManager;
               _accessor = accessor;
          }

          //[Authorize(AuthenticationSchemes = "Bearer")]
          [HttpGet("testUser")]
          public async Task<IActionResult> YourMethodName()
          {
               var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
               var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

               AppUser applicationUser = await _userManager.GetUserAsync(User);
               string userEmail = applicationUser?.Email; // will give the user's Email

               return null;
          }
     }
}
