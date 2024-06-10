using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.News
{
    public class IndexModel : PageModel
    {
        private readonly INewsService _newsService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int _pageSize = 5;

        public IndexModel(INewsService newsService, IHttpContextAccessor httpContextAccessor)
        {
            _newsService = newsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IList<NewsArticleDto> NewsArticle { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(int currentPage = 1)
        {
            var role = _httpContextAccessor.HttpContext?.Session?.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Staff" != role)
                return RedirectToPage("../StaffRedirect");

            NewsArticle = (IList<NewsArticleDto>)await _newsService.GetNewsV2();
            TotalPages = (int)Math.Ceiling(NewsArticle.Count() / (double)_pageSize);

            CurrentPage = currentPage;
            NewsArticle = NewsArticle.Skip((CurrentPage - 1) * _pageSize).Take(_pageSize).ToList();
            return Page();  
        }
    }
}
