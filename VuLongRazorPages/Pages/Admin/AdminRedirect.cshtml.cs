using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Admin
{
    public class AdminRedirectModel : BasePageModel
    {
        private readonly IAccountService _accountService;
        private readonly int _pageSize = 5;

        public AdminRedirectModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IList<SystemAccountDto> Accounts { get; private set; } = null!;

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        protected override string RequiredRole => "Admin";

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
