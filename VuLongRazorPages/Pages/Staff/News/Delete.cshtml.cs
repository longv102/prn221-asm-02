using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.News
{
    public class DeleteModel : PageModel
    {
        private readonly INewsService _newsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteModel(INewsService newsService, IHttpContextAccessor httpContextAccessor)
        {
            _newsService = newsService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public NewsArticleDto NewsArticle { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            #region
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Staff" != role)
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _newsService.DeleteNews(id);
            switch (result)
            {
                case BO.Enums.NewsOperationResult.Success:
                    return RedirectToPage("./Index");
                case BO.Enums.NewsOperationResult.Empty:
                    ModelState.AddModelError(string.Empty, "News is empty!");
                    break;
            }
            return Page();
        }
    }
}
