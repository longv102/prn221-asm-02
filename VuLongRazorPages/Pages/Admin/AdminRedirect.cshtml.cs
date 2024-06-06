using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Admin
{
    public class AdminRedirectModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminRedirectModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<SystemAccountDto> Accounts { get; private set; } = null!;

        public string GetRoleName(int accountRole)
        {
            return accountRole switch
            {
                1 => "Staff",
                2 => "Lecturer",
                _ => "Unknown"
            };
        }

        public async Task<ActionResult> OnGetAsync()
        {
            // Authorize for admin
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Admin" != role)
            {
                return RedirectToPage("../Index");
            }
            Accounts = (IList<SystemAccountDto>) await _accountService.GetAccounts();
            return Page();
        }
    }
}
