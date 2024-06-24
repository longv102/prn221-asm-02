using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.News
{
    public class CreateModel : BasePageModel
    {
        private readonly INewsService _newsService;
        private readonly IAccountService _accountService;
        private readonly ICategoryService _categoryService;

        public CreateModel(INewsService newsService, IAccountService accountService, ICategoryService categoryService)
        {
            _newsService = newsService;
            _accountService = accountService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> OnGetAsync()
        {   
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName");
            ViewData["CreatedById"] = new SelectList(await _accountService.GetAccounts(), "AccountId", "AccountName");
            return Page();
        }

        [BindProperty]
        public NewsArticleDto NewsArticle { get; set; } = default!;

        protected override string RequiredRole => "Staff";

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _newsService.CreateNews(NewsArticle);
            switch (result)
            {
                case BO.Enums.NewsOperationResult.Success:
                    return RedirectToPage("./Index");
                case BO.Enums.NewsOperationResult.Duplicate:
                    ModelState.AddModelError(string.Empty, "News id is already existed!");
                    // Reload the data of category and account
                    ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName");
                    ViewData["CreatedById"] = new SelectList(await _accountService.GetAccounts(), "AccountId", "AccountName");
                    break;
                case BO.Enums.NewsOperationResult.Empty:
                    ModelState.AddModelError(string.Empty, "News id is empty!");
                    // Reload the data of category and account
                    ViewData["CategoryId"] = new SelectList(await _categoryService.GetCategories(), "CategoryId", "CategoryName");
                    ViewData["CreatedById"] = new SelectList(await _accountService.GetAccounts(), "AccountId", "AccountName");
                    break;
            }
            return Page();
        }
    }
}
