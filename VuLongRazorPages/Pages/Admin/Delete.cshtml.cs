using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public SystemAccountDto SystemAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            // Authorize
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Admin" != role)
            {
                return RedirectToPage("../Index");
            }

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
