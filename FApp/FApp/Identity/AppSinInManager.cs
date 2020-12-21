using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FApp.DAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FApp
{
    public class AppSinInManager : SignInManager<User>
    {
        public AppSinInManager(UserManager<User> userManager, 
            IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<User> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override Task<bool> CanSignInAsync(User user)
        {
            return Task.FromResult(!user.IsBlocked);
        }

        protected override async Task<SignInResult> PreSignInCheck(User user)
        {
            var u = UserManager.Users.SingleOrDefault(x => x.Id == user.Id);
            if (u == null || u.IsBlocked)
            {
                await SignOutAsync();
                return SignInResult.NotAllowed;
            }

            return await base.PreSignInCheck(user);
        }
    }
}
