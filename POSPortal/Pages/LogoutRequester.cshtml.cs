using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace POSPortal.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Posting logout..");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Log.Information($" User logged out successfuly");
            return LocalRedirect(Url.Content("~/RequestStatus"));
        }
    }
}
