using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchOption { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public IList<CategoryDto> Category { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            // Authorization for staff role
            var role = _httpContextAccessor.HttpContext?.Session?.GetString("Role");
            if (string.IsNullOrEmpty(role) || "Staff" != role)
                return RedirectToPage("../StaffRedirect");
            
            Category = (IList<CategoryDto>)await _categoryService.GetCategories();
            if (!string.IsNullOrEmpty(SearchQuery))
            {
                if ("Name" == SearchOption)
                {
                    Category = Category.Where(x => x.CategoryName.Contains(SearchQuery.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                    return Page();
                }
                else if ("Description" == SearchOption)
                {
                    Category = Category.Where(x => x.CategoryDesciption.Contains(SearchQuery.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();
                    return Page();
                }
            }
            return Page();
        }
    }
}
