using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly INewsService _newsService; // number of items per page
        private const int _pageSize = 5;

        public IndexModel(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IEnumerable<NewsArticleDto> News { get; set; } = null!;

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public async Task<ActionResult> OnGetAsync(int currentPage = 1)
        {
            News = await _newsService.GetNews();
            TotalPages = (int)Math.Ceiling(News.Count() / (double)_pageSize);
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                News = News.Where(n => n.NewsTitle.Contains(SearchQuery.Trim(), StringComparison.OrdinalIgnoreCase));
            }
            CurrentPage = currentPage;
            News = News.Skip((CurrentPage - 1) * _pageSize).Take(_pageSize).ToList();
            return Page();
        }
    }
}
