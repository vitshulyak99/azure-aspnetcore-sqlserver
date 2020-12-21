using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FApp.DAL;
using FApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FApp.Controllers
{
    [Authorize]
    [Route("user")]
    public class UserController : Controller
    {

        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("lock")]
        public async Task<IActionResult> BlockUsers()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIds = Request.Form["user-id"].ToArray();
                if (userIds == null) return Redirect("/");
                await UpdateUser(x => x.IsBlocked = true, userIds);
            }

            return Redirect("/");
        }

        [HttpPost("unlock")]
        public async Task<IActionResult> UnBlockUsers()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIds = Request.Form["user-id"].ToArray();
                if (userIds == null) return Redirect("/Home/Index");
                await UpdateUser(x => x.IsBlocked = false, userIds);
            }

            return Redirect("/");
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIds = Request.Form["user-id"].ToArray();

                if (userIds == null) return Redirect("/");

                foreach (var user in GetUsers(userIds))
                {
                    await _userManager.DeleteAsync(user);
                }
            }

            return Redirect("/");
        }

        private List<User> GetUsers(string[] ids) =>
            _userManager.Users
                .Where(x => ids.Contains(x.Id))
                .ToList();

        private async Task UpdateUser([NotNull] Action<User> action, [NotNull] string[] ids)
        {
            foreach (var user in GetUsers(ids))
            {
                action(user);
                await _userManager.UpdateAsync(user);
            }
        }
    }
}