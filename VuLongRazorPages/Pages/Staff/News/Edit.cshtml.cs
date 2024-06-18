using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.News
{
    public class EditModel : PageModel
    {
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EditModel(INewsService newsService, ICategoryService categoryService, IAccountService accountService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _newsService = newsService;
            _categoryService = categoryService;
            _accountService = accountService;
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

            NewsArticle = newsArticle;
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName");
            ViewData["CreatedById"] = new SelectList(await _accountService.GetAccounts(), "AccountId", "AccountName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _newsService.UpdateNews(NewsArticle);
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
