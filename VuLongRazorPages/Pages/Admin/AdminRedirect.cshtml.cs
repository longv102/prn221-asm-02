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
        private readonly int _pageSize = 5;

        public AdminRedirectModel(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<SystemAccountDto> Accounts { get; private set; } = null!;

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string GetRoleName(int accountRole)
        {
            return accountRole switch
            {
                1 => "Staff",
                2 => "Lecturer",
                _ => "Unknown"
            };
        }

        public async Task<ActionResult> OnGetAsync(int currentPage = 1)
        {
            // Authorize for admin
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Admin" != role)
            {
                return RedirectToPage("../Index");
            }

            Accounts = (IList<SystemAccountDto>) await _accountService.GetAccounts();
            TotalPages = (int)Math.Ceiling(Accounts.Count() / (double)_pageSize);
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                Accounts = Accounts
                    .Where(x => x.AccountName.Contains(SearchQuery.Trim(), StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            CurrentPage = currentPage;
            Accounts = Accounts.Skip((CurrentPage - 1) * _pageSize).Take(_pageSize).ToList();
            return Page();
        }
    }
}
