using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using POSPortal.Resources;
using Serilog;

namespace POSPortal.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    { 
        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                ActiveDirectory ad = new ActiveDirectory();
                LoginResponseResource res = await ad.Authenticate(username, password);
                //if (username == password.Substring(3))
                //    res = new LoginResponseResource()
                //    {
                //        scopeLevel = new ScopeLevel
                //        { branchcode = password.Substring(0, 3) },
                //        Success = true,
                //        token = "",
                //        user = new User { sAMAccountName = username.Replace('.',' ') }
                //    };
                //else
                //    res = await ad.Authenticate(username, password);

                if (res.Success)
                {
                    var claims = new List<Claim>{
                        new Claim(ClaimTypes.Name, res.user.sAMAccountName),
                        new Claim("BranchCode", res.scopeLevel.branchcode)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, principal,
                         new AuthenticationProperties
                         {
                             ExpiresUtc = DateTime.UtcNow.AddMinutes(10),
                             IsPersistent = false
                         });

                    //await HttpContext.SignInAsync(
                    //    CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    Log.Information($"{username} logged in");
                    return LocalRedirect(Url.Content($"~/RequestStatus/{res.scopeLevel.branchcode}"));
                }
                else
                {
                    //Log.Information($"{username} failed authentication");
                    return LocalRedirect(Url.Content("~/RequestStatus/Unauthorized"));
                }
            }
            else
            {
                return LocalRedirect(Url.Content("~/RequestStatus/blank"));
            }
        }
    }
}