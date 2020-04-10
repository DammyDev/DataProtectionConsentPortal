using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace ConsentPortal.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    { 
        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            Console.WriteLine("Posting here..");
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {

                ActiveDirectory ad = new ActiveDirectory();
                bool res = ad.Authenticate("Wemabank", username, password);

                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, username)
                    //new Claim("Role", "user"),

                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
               
                if (res)
                {
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    Log.Information($"{username} logged in");
                    return LocalRedirect(Url.Content("~/"));
                }
                else
                {
                    Log.Information($"{username} failed authentication");
                    return LocalRedirect(Url.Content("~/Unauthorized"));
                }
            }
            else
            {
                return LocalRedirect(Url.Content("~/blank"));
            }
        }
    }
}