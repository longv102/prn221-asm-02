using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff
{
    public class StaffProfileModel : PageModel
    {
        private readonly IAccountService _accountService;

        public StaffProfileModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public SystemAccountDto SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string email)
        {
            var account = await _accountService.GetAccount(email);
            if (account is null)
            {
                return NotFound();
            }
            SystemAccount = account;
            return Page();
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
                    return RedirectToPage("../StaffRedirect");
                case BO.Enums.AccountOperationResult.EmptyAccount:
                    ModelState.AddModelError(string.Empty, "Error operation!");
                    break;
            }
            return Page();

        }
    }
}
