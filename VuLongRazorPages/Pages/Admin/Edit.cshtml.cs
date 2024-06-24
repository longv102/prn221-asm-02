using BO.Dtos;
using BO.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Admin
{
    public class EditModel : BasePageModel
    {
        private readonly IAccountService _accountService;

        public EditModel(IAccountService accountService)
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
                case AccountOperationResult.Success:
                    return RedirectToPage("./AdminRedirect");
                case AccountOperationResult.EmptyAccount:
                    ModelState.AddModelError(string.Empty, "Error! Account is null.");
                    break;
            }
            return Page();
        }
    }
}
