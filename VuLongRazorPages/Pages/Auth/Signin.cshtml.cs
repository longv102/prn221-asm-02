using BO.Constants;
using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Auth
{
    public class SigninModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public AuthRequest AuthRequest { get; set; } = null!;

        public SigninModel(IAccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _configuration = configuration;
        }

        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPostAsync()
        {
            // Read the FU News admin credentials stored in appsettings.json
            var adminEmail = _configuration["FUNewsAdminAccount:Email"];
            var adminPassword = _configuration["FUNewsAdminAccount:Password"];
            var adminRole = _configuration["FUNewsAdminAccount:Role"] ?? string.Empty;

            if (!ModelState.IsValid) return Page();
            // Authentication
            if (adminEmail == AuthRequest.Email && adminPassword == AuthRequest.Password)
            {
                HttpContext.Session.SetString("AdminEmail", AuthRequest.Email);
                HttpContext.Session.SetString("Role", adminRole);
                // Redirects to admin page
                return RedirectToPage("/Admin/AdminRedirect");
            }
            
            var account = await _accountService.Authenticate(AuthRequest);
            if (account is not null)
            {
                // Role authentication
                if (account.AccountRole == AccountRole.StaffRole)
                {
                    HttpContext.Session.SetString("StaffEmail", account.AccountEmail);
                    HttpContext.Session.SetString("StaffName", account.AccountName);
                    HttpContext.Session.SetString("Role", "Staff");
                    // Redirects to staff page
                    return RedirectToPage("/Staff/StaffRedirect");
                }

            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
