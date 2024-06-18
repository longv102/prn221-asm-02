using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.News
{
    public class DetailsModel : PageModel
    {
        private readonly INewsService _newsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsModel(INewsService newsService, IHttpContextAccessor httpContextAccessor)
        {
            _newsService = newsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public NewsArticleDto NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            #region
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Admin" != role)
            {
                return RedirectToPage("../Index");
            }
            #endregion

            if (id == null)
            {
                return NotFound();
            }

            var newsArticle = await _newsService.GetNewsById(id);
            if (newsArticle == null)
            {
                return NotFound();
            }
            else
            {
                NewsArticle = newsArticle;
            }
            return Page();
        }
    }
}
