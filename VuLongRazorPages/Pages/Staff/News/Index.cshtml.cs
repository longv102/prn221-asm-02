using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.News
{
    public class IndexModel : BasePageModel
    {
        private readonly INewsService _newsService;
        private readonly int _pageSize = 5;

        public IndexModel(INewsService newsService)
        {
            _newsService = newsService;
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public IList<NewsArticleDto> NewsArticle { get;set; } = default!;

        protected override string RequiredRole => "Staff";

        public async Task<IActionResult> OnGetAsync(int currentPage = 1)
        {   
            NewsArticle = (IList<NewsArticleDto>)await _newsService.GetNewsV2();
            TotalPages = (int)Math.Ceiling(NewsArticle.Count() / (double)_pageSize);

            CurrentPage = currentPage;
            NewsArticle = NewsArticle.Skip((CurrentPage - 1) * _pageSize).Take(_pageSize).ToList();
            return Page();  
        }
    }
}
