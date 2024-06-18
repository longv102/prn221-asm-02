using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace VuLongRazorPages.Pages.Admin
{
    public class ReportPageModel : PageModel
    {
        private readonly INewsService _newsService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReportPageModel(INewsService newsService, IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _newsService = newsService;
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<SystemAccountDto> Accounts { get; set; } = default!;

        public IList<NewsArticleDto>? NewsArticles { get; set; }

        [BindProperty]
        public short SelectedAccountId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "From date is required.")]
        public DateTime? FromDate { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "To date is required.")]
        public DateTime? ToDate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            #region
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Admin" != role)
            {
                return RedirectToPage("../Index");
            }
            #endregion
            Accounts = (IList<SystemAccountDto>)await _accountService.GetAccounts();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Accounts = (IList<SystemAccountDto>)await _accountService.GetAccounts();
                return Page();
            }
            NewsArticles = await _newsService.MakeNewsReport(SelectedAccountId, fromDate: (DateTime)FromDate, toDate: (DateTime)ToDate);
            if (!NewsArticles.Any())
            {
                // Reload
                Accounts = (IList<SystemAccountDto>)await _accountService.GetAccounts();
                return Page();
            }

            Accounts = (IList<SystemAccountDto>)await _accountService.GetAccounts();
            return Page();
        }
    }
}
