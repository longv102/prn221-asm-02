using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff
{
    public class NewsHistoryModel : BasePageModel
    {
        private readonly INewsService _newsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewsHistoryModel(INewsService newsService, IHttpContextAccessor httpContextAccessor)
        {
            _newsService = newsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<NewsArticleDto> NewsArticles { get; set; } = default!;

        protected override string RequiredRole => "Staff";

        public async Task<IActionResult> OnGetAsync()
        {
            // Retrieve email from session 
            var staffEmail = _httpContextAccessor.HttpContext?.Session?.GetString("StaffEmail") ?? string.Empty;
            NewsArticles = (IList<NewsArticleDto>)await _newsService.GetNewsByStaffEmail(staffEmail);

            return Page();
        }
    }
}
