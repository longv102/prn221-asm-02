using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Admin
{
    public class DeleteModel : BasePageModel
    {
        private readonly IAccountService _accountService;

        public DeleteModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public SystemAccountDto SystemAccount { get; set; } = default!;

        protected override string RequiredRole => "Admin";

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _accountService.GetAccount((short)id);

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                SystemAccount = account;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _accountService.DeleteAccount((short)id);
            switch (result)
            {
                case BO.Enums.AccountOperationResult.Success:
                    return RedirectToPage("./AdminRedirect");
                case BO.Enums.AccountOperationResult.EmptyAccount:
                    ModelState.AddModelError(string.Empty, "Error! Account is null.");
                    break;
            }
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
    }
}
