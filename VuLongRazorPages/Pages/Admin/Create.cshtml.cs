using BO.Dtos;
using BO.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Admin
{
    public class CreateModel : BasePageModel
    {
        private readonly IAccountService _accountService;

        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public SystemAccountDto SystemAccount { get; set; } = default!;

        protected override string RequiredRole => "Admin";

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
