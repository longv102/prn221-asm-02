using BO.Dtos;
using BO.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public SystemAccountDto SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            #region Authorize
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Admin" != role)
            {
                return RedirectToPage("../Index");
            }
            #endregion

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
