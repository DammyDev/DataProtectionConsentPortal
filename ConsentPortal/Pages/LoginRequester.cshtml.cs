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

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {

                ActiveDirectory ad = new ActiveDirectory();
               // bool res = ad.Authenticate("Wemabank", username, password);
                bool res = true;

                var claims = new List<Claim>{
                    new Claim(ClaimTypes.Name, username)
                    //new Claim("Role", "ninja"),

                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
               
                if (res)
                {
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    Log.Information($"{username} logged in");
                    return LocalRedirect(Url.Content("~/ConsentPage"));
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

    //public string ReturnUrl { get; set; }

    //public async Task<IActionResult>
    //    OnGetAsync(string paramUsername, string paramPassword)
    //    {
    //    string username = EncryptionClass.Decrypt(paramUsername, "wema");
    //    string password = EncryptionClass.Decrypt(paramPassword, "wema");

    //    string returnUrl = Url.Content("~/");
    //    try
    //    {
    //        // Clear the existing external cookie
    //        await HttpContext
    //            .SignOutAsync(
    //            CookieAuthenticationDefaults.AuthenticationScheme);
    //    }
    //    catch { }
    //    // *** !!! This is where you would validate the user !!! ***
    //    // In this example we just log the user in
    //    // (Always log the user in for this demo)

    //    ActiveDirectory ad = new ActiveDirectory();
    //   // bool res = ad.Authenticate("Wemabank", paramUsername, paramPassword);
    //    bool res = true;

    //    var claims = new List<Claim>
    //    {
    //        new Claim(ClaimTypes.Name, paramUsername),
    //        new Claim(ClaimTypes.Role, "Administrator"),
    //    };

    //    var claimsIdentity = new ClaimsIdentity(
    //        claims, CookieAuthenticationDefaults.AuthenticationScheme);

    //    var authProperties = new AuthenticationProperties
    //    {
    //        IsPersistent = true,
    //        RedirectUri = this.Request.Host.Value
    //    };
    //    try
    //    {
    //        if (res)
    //        {
    //            await HttpContext.SignInAsync(
    //            CookieAuthenticationDefaults.AuthenticationScheme,
    //            new ClaimsPrincipal(claimsIdentity),
    //            authProperties);
    //        }

    //    }
    //    catch (Exception ex)
    //    {
    //        string error = ex.Message;
    //    }
    //    return LocalRedirect(returnUrl);
    //}
}
}