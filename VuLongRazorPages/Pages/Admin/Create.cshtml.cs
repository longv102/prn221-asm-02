using BO.Dtos;
using BO.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            // Authorize
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Admin" != role)
            {
                return RedirectToPage("../Index");
            }
            return Page();
        }

        [BindProperty]
        public SystemAccountDto SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _accountService.CreateAccount(SystemAccount);
            switch (result)
            {
                case AccountOperationResult.Success:
                    return RedirectToPage("./AdminRedirect");
                case AccountOperationResult.AccountExist:
                    ModelState.AddModelError(string.Empty, "Account's id is already existed!");
                    break;
                case AccountOperationResult.EmailExist:
                    ModelState.AddModelError(string.Empty, "Email is already existed! Please provide another email.");
                    break;
                case AccountOperationResult.EmptyAccount:
                    ModelState.AddModelError(string.Empty, "Error! Account is null.");
                    break;
            }
            return Page();
        }
    }
}
