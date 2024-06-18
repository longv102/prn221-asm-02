using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff
{
    public class StaffProfileModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StaffProfileModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public SystemAccountDto SystemAccount { get; set; } = default!;

        public string? Role { get; set; }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            #region Authorize
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Staff" != role)
            {
                return RedirectToPage("../Index");
            }
            #endregion

            var account = await _accountService.GetAccount(email);
            if (account is null)
            {
                return NotFound();
            }
            SystemAccount = account;
            return Page();
        }

        public string GetRoleName(int accountRole)
        {
            return accountRole switch
            {
                1 => "Staff",
                2 => "Lecturer",
                _ => "Unknown"
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _accountService.UpdateAccount(SystemAccount);
            switch (result)
            {
                case BO.Enums.AccountOperationResult.Success:
                    return RedirectToPage("./StaffRedirect");
                case BO.Enums.AccountOperationResult.EmptyAccount:
                    ModelState.AddModelError(string.Empty, "Error operation!");
                    break;
            }
            return Page();

        }
    }
}
