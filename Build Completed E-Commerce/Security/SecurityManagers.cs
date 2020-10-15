
using Build_Completed_E_Commerce.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace Build_Completed_E_Commerce.Security
{
    public class SecurityManagers
    {
        public async void SignIn(HttpContext httpContext, Account acount)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(GetUserClaims(acount),CookieAuthenticationDefaults.AuthenticationScheme);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        }
        public async void SignOut(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }
            private IEnumerable<Claim> GetUserClaims(Account acount)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, acount.Username));
            acount.RoleAccounts.ToList().ForEach(ra =>
            {
                claims.Add(new Claim(ClaimTypes.Role, ra.Role.Name));
            });
            return claims;
        }
    }
}
