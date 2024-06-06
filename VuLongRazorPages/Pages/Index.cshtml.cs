using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly INewsService _newsService;

        public IndexModel(INewsService newsService)
        {
            _newsService = newsService;
        }

        public IEnumerable<NewsArticleDto> News { get; set; } = null!;
        
        public async Task<ActionResult> OnGetAsync()
        {
            News = await _newsService.GetNews();
            return Page();
        }
    }
}
