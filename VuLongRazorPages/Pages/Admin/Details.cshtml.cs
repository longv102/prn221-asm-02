using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly IAccountService _accountService;

        public DetailsModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public SystemAccountDto SystemAccount { get; set; } = default!;

        public string GetRoleName(int accountRole)
        {
            return accountRole switch
            {
                1 => "Staff",
                2 => "Lecturer",
                _ => "Unknown"
            };
        }

        public async Task<IActionResult> OnGetAsync(short? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var systemAccount = await _accountService.GetAccount((short)id);
            if (systemAccount == null)
            {
                return NotFound();
            }
            else
            {
                SystemAccount = systemAccount;
            }
            return Page();
        }
    }
}
