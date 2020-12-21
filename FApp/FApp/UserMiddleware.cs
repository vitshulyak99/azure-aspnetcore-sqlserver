using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FApp.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FApp
{
    public class UserMiddleware :IMiddleware
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSinInManager _sinInManager; 
        public UserMiddleware(UserManager<User> userManager, AppSinInManager sinInManager)
        {
            _userManager = userManager;
            _sinInManager = sinInManager;
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var name = context?.User?.Identities.FirstOrDefault()?.Name;
            
            if (!string.IsNullOrEmpty(name) )
            {
                var user = await _userManager.FindByNameAsync(name);
                if (user == null || user.IsBlocked)
                {
                    await _sinInManager.SignOutAsync();
                    context.Response.Redirect("/identity/account/login");
                }
                else
                {
                    await next(context!);
                }
            }
            else
            {
                await next(context!);
            }
        }
    }
}
