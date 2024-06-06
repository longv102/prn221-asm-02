using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages
{
    public class NewsDetailModel : PageModel
    {
        private readonly INewsService _newsService;

        public NewsDetailModel(INewsService newsService)
        {
            _newsService = newsService;
        }

        public NewsArticleDto News { get; private set; } = null!;

        public async Task<ActionResult> OnGetAsync(string id)
        {
            News = await _newsService.GetNewsById(id);
            return Page();
        }
    }
}
