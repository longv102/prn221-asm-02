using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.News
{
    public class ManageTagsModel : PageModel
    {
        private readonly INewsService _newsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ManageTagsModel(INewsService newsService, IHttpContextAccessor httpContextAccessor)
        {
            _newsService = newsService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<TagDto>? Tags { get; set; }

        public NewsArticleDto NewsArticle { get; set; } = default!;

        [BindProperty]
        public IList<int> SelectedTagIds { get; set; } = new List<int>();

        public async Task<IActionResult> OnGetAsync(string id)
        {

            #region
            var role = _httpContextAccessor.HttpContext?.Session.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Staff" != role)
            {
                return RedirectToPage("../Index");
            }
            #endregion
            if (string.IsNullOrEmpty(id))
                return NotFound();

            NewsArticle = await _newsService.GetNewsById(id);
            Tags = await _newsService.GetTags();
            SelectedTagIds = await _newsService.GetTagOfANewsArticle(id);

            return Page();
        }

        // TODO: Check duplicate, layout error when a news does not have any tags
        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            if (!ModelState.IsValid)
            {
                // Reload
                NewsArticle = await _newsService.GetNewsById(id);
                Tags = await _newsService.GetTags();
                SelectedTagIds = await _newsService.GetTagOfANewsArticle(id);
            }

            if (!SelectedTagIds.Any())
            {
                ModelState.AddModelError(string.Empty, "News must have at least one tag.");
                // Reload
                NewsArticle = await _newsService.GetNewsById(id);
                Tags = await _newsService.GetTags();
                SelectedTagIds = await _newsService.GetTagOfANewsArticle(id);
            }
            else
            {
                var result = await _newsService.UpdateTags(id, SelectedTagIds);
                switch (result)
                {
                    case BO.Enums.NewsOperationResult.Success:
                        return RedirectToPage("./Index");
                    case BO.Enums.NewsOperationResult.Empty:
                        ModelState.AddModelError(string.Empty, "Empty.");
                        break;
                }
            }
            return Page();
        }
    }
}
