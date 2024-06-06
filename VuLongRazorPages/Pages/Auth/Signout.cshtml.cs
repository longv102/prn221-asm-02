using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VuLongRazorPages.Pages.Auth
{
    public class SignoutModel : PageModel
    {
        public async Task<ActionResult> OnGetAsync()
        {
            // clear the session
            HttpContext.Session.Clear();

            // Delete the session cookie
            Response.Cookies.Delete(".AspNetCore.Session");

            // sign out the user
            await HttpContext.SignOutAsync();

            return RedirectToPage("../Index");
        }
    }
}
