using BO.Dtos;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace VuLongRazorPages.Pages.Staff.Categories
{
    public class IndexModel : BasePageModel
    {
        private readonly ICategoryService _categoryService;
        
        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchOption { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchQuery { get; set; }

        public IList<CategoryDto> Category { get;set; } = default!;

        protected override string RequiredRole => "Staff";

        public async Task<IActionResult> OnGetAsync()
        {    
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
